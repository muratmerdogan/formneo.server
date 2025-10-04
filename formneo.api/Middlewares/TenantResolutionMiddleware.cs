using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using formneo.core.Services;
using formneo.core.Options;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Identity;
using formneo.core.Models;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace formneo.api.Middlewares
{
	public class TenantResolutionMiddleware
	{
		private readonly RequestDelegate _next;
		public const string HeaderName = "X-Tenant-Id";

		public TenantResolutionMiddleware(RequestDelegate next)
		{
			_next = next;
		}

		public async Task Invoke(HttpContext context, ITenantContext tenantContext, IUserService userService, IUserTenantService userTenantService, IOptions<RoleScopeOptions> roleScopeOptions, UserManager<UserApp> userManager, RoleManager<IdentityRole> roleManager, IMemoryCache cache, ILogger<TenantResolutionMiddleware> logger)
		{

            //await _next(context);
            tenantContext.CurrentTenantId = null;
			Stopwatch sw = null;
			if (logger != null && logger.IsEnabled(LogLevel.Debug))
			{
				sw = Stopwatch.StartNew();
			}

			// Fast-path: OPTIONS ve statik/health uçlarını atla
			var path = context.Request.Path.Value ?? string.Empty;
            if (HttpMethods.IsOptions(context.Request.Method)
				|| path.StartsWith("/swagger", StringComparison.OrdinalIgnoreCase)
				|| path.StartsWith("/health", StringComparison.OrdinalIgnoreCase)
				|| path.Equals("/favicon.ico", StringComparison.OrdinalIgnoreCase)
				|| path.StartsWith("/static", StringComparison.OrdinalIgnoreCase)
				|| path.StartsWith("/assets", StringComparison.OrdinalIgnoreCase)
                // Getir webhooks: tenant ve auth akışını baypas et (X-Api-Key ile doğrulanıyor)
                || path.StartsWith("/webhooks/getir", StringComparison.OrdinalIgnoreCase)
				// Login/Token uçlarını tenant çözümlemeden baypas et
				|| path.StartsWith("/api/auth", StringComparison.OrdinalIgnoreCase)
				|| path.Equals("/connect/token", StringComparison.OrdinalIgnoreCase)
				|| path.Equals("/signin-oidc", StringComparison.OrdinalIgnoreCase)
				// Tenant seçim ekranı: kullanıcıya ait tenant listesini dönen uç
				|| path.StartsWith("/api/UserTenants/by-user", StringComparison.OrdinalIgnoreCase)
				// Giriş yapan kullanıcının detayları: tenant seçimi öncesi kullanılabilir
				|| path.StartsWith("/api/User/GetLoginUserDetail", StringComparison.OrdinalIgnoreCase)
				// UserController: is-global-admin ucu icin baypas
				|| path.StartsWith("/api/User/is-global-admin", StringComparison.OrdinalIgnoreCase))
			{
				await _next(context);
				if (sw != null)
				{
					sw.Stop();
					logger?.LogDebug("TenantResolution took {ElapsedMs} ms (tenant={TenantId})", sw.ElapsedMilliseconds, tenantContext.CurrentTenantId);
				}
				return;
			}

			// Authorization header geliyor olsa da principal dolu olmayabilir; sadece gerekirse authenticate et
			if (context.User?.Identity?.IsAuthenticated != true)
			{
				var authResult = await context.AuthenticateAsync("MultiAuth");
				if (authResult.Succeeded && authResult.Principal != null)
				{
					context.User = authResult.Principal;
				}
			}

			// Kullanıcıyı çöz ve global admin mi belirle (RoleScopeOptions.GlobalOnlyRoleIds'e göre)
			var principal = context.User;
			if (principal?.Identity?.IsAuthenticated == true)
			{
				var userIdClaim = principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
				UserApp? identityUserForRoleCheck = null;
				if (!string.IsNullOrWhiteSpace(userIdClaim))
				{
					identityUserForRoleCheck = await userManager.FindByIdAsync(userIdClaim);
				}
				if (identityUserForRoleCheck == null)
				{
					var email = principal.FindFirst(ClaimTypes.Email)?.Value
						?? principal.FindFirst("preferred_username")?.Value
						?? principal.Identity?.Name;
					if (!string.IsNullOrWhiteSpace(email))
					{
						identityUserForRoleCheck = await userManager.FindByEmailAsync(email);
						if (identityUserForRoleCheck == null)
						{
							identityUserForRoleCheck = await userManager.FindByNameAsync(email);
						}
					}
				}

				var globalOnlyRoleIds = roleScopeOptions?.Value?.GlobalOnlyRoleIds ?? new System.Collections.Generic.List<string>();
				bool isGlobalAdmin = false;
				if (identityUserForRoleCheck != null && globalOnlyRoleIds.Count > 0)
				{
					var cacheKeyGlobal = $"global-admin:{identityUserForRoleCheck.Id}";
					if (!cache.TryGetValue(cacheKeyGlobal, out isGlobalAdmin))
					{
						var roleNames = await userManager.GetRolesAsync(identityUserForRoleCheck);
						if (roleNames != null && roleNames.Count > 0)
						{
							var roleIds = roleManager.Roles.Where(r => roleNames.Contains(r.Name)).Select(r => r.Id).ToList();
							isGlobalAdmin = roleIds.Any(id => globalOnlyRoleIds.Contains(id));
						}
						cache.Set(cacheKeyGlobal, isGlobalAdmin, TimeSpan.FromMinutes(10));
					}
				}

				// Make global-admin info available to downstream components (e.g., EF interceptors)
				context.Items["IsGlobalAdmin"] = isGlobalAdmin;

				// Header yok veya boşsa: GET/HEAD serbest, yazmalarda header zorunlu (global admin hariç)
				if (context.Request.Headers.TryGetValue(HeaderName, out var hv))
				{
					var headerVal = (hv.FirstOrDefault() ?? string.Empty).Trim();
					if (string.IsNullOrEmpty(headerVal))
					{
						if (HttpMethods.IsGet(context.Request.Method) || HttpMethods.IsHead(context.Request.Method))
						{
							await _next(context);
							if (sw != null)
							{
								sw.Stop();
								logger?.LogDebug("TenantResolution took {ElapsedMs} ms (tenant={TenantId})", sw.ElapsedMilliseconds, tenantContext.CurrentTenantId);
							}
							return;
						}

						// Global admin ise: tenant header olmadan yazmaya izin ver (global varlıklar için)
			
						if (!isGlobalAdmin)
						{
							context.Response.StatusCode = StatusCodes.Status400BadRequest;
							await context.Response.WriteAsync("X-Tenant-Id header required for write operations");
							return;
						}
					}
				}
				else
				{
					if (HttpMethods.IsGet(context.Request.Method) || HttpMethods.IsHead(context.Request.Method))
					{
						await _next(context);
						if (sw != null)
						{
							sw.Stop();
							logger?.LogDebug("TenantResolution took {ElapsedMs} ms (tenant={TenantId})", sw.ElapsedMilliseconds, tenantContext.CurrentTenantId);
						}
						return;
					}

					// Global admin ise: tenant header olmadan yazmaya izin ver (global varlıklar için)
	
					if (!isGlobalAdmin)
					{
						context.Response.StatusCode = StatusCodes.Status400BadRequest;
						await context.Response.WriteAsync("X-Tenant-Id header required for write operations");
						return;
					}
				}
			}

			if (context.Request.Headers.TryGetValue(HeaderName, out var values))
			{
				var headerValue = (values.FirstOrDefault() ?? string.Empty).Trim();
				if (string.IsNullOrEmpty(headerValue))
				{
					// Boş header daha yukarıda ele alındı; buraya düşerse akışı sürdür
					await _next(context);
					if (sw != null)
					{
						sw.Stop();
						logger?.LogDebug("TenantResolution took {ElapsedMs} ms (tenant={TenantId})", sw.ElapsedMilliseconds, tenantContext.CurrentTenantId);
					}
					return;
				}
				if (Guid.TryParse(headerValue, out var tenantId))
				{
					if (principal?.Identity?.IsAuthenticated == true)
					{
						var userIdFromClaim = principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
						UserApp? identityUser = null;
						if (!string.IsNullOrWhiteSpace(userIdFromClaim))
						{
							identityUser = await userManager.FindByIdAsync(userIdFromClaim);
						}
						if (identityUser == null)
						{
							var emailOrName = principal.FindFirst(ClaimTypes.Email)?.Value
								?? principal.FindFirst("preferred_username")?.Value
								?? principal.Identity?.Name;
							if (!string.IsNullOrWhiteSpace(emailOrName))
							{
								identityUser = await userManager.FindByEmailAsync(emailOrName) ?? await userManager.FindByNameAsync(emailOrName);
							}
						}

						if (identityUser != null)
						{
							var cacheKeyMembership = $"user-tenant:{identityUser.Id}:{tenantId}";
							bool hasMembership;
							if (!cache.TryGetValue(cacheKeyMembership, out hasMembership))
							{
								var link = await userTenantService.GetByUserAndTenantAsync(identityUser.Id, tenantId);
								hasMembership = link != null && link.TenantId == tenantId && link.UserId == identityUser.Id && link.IsActive;
								cache.Set(cacheKeyMembership, hasMembership, TimeSpan.FromMinutes(5));
							}
							if (hasMembership)
							{
								tenantContext.CurrentTenantId = tenantId;
							}
							else
							{
								// Global admin ise üyelik şartını baypas et
								var globalOnlyRoleIds2 = roleScopeOptions?.Value?.GlobalOnlyRoleIds ?? new System.Collections.Generic.List<string>();
								bool isGlobalAdmin2 = false;
								var roleNames2 = await userManager.GetRolesAsync(identityUser);
								if (roleNames2 != null && roleNames2.Count > 0)
								{
									var roleIds2 = roleManager.Roles.Where(r => roleNames2.Contains(r.Name)).Select(r => r.Id).ToList();
									isGlobalAdmin2 = roleIds2.Any(id => globalOnlyRoleIds2.Contains(id));
								}

								if (isGlobalAdmin2)
								{
									tenantContext.CurrentTenantId = tenantId;
								}
								else
								{
									context.Response.StatusCode = StatusCodes.Status403Forbidden;
									await context.Response.WriteAsync("Tenant access denied");
									return;
								}
							}
						}
					}
				}
				else
				{
					context.Response.StatusCode = StatusCodes.Status400BadRequest;
					await context.Response.WriteAsync("Invalid X-Tenant-Id header");
					return;
				}
			}

			await _next(context);
			if (sw != null)
			{
				sw.Stop();
				logger?.LogDebug("TenantResolution took {ElapsedMs} ms (tenant={TenantId})", sw.ElapsedMilliseconds, tenantContext.CurrentTenantId);
			}
		}
	}
}


