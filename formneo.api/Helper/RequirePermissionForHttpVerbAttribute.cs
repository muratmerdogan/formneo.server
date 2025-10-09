using System;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using formneo.core.Models.Security;
using formneo.core.Services;

namespace formneo.api.Helper
{
    // Maps HTTP verbs to permission actions and authorizes using the configured permission policy provider
    public sealed class RequirePermissionForHttpVerbAttribute : Attribute, IAsyncAuthorizationFilter
    {
        private readonly string _resourceKey;

        public RequirePermissionForHttpVerbAttribute(string resourceKey)
        {
            _resourceKey = resourceKey;
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            // Skip permission checks for anonymous endpoints explicitly marked
            var endpoint = context.HttpContext.GetEndpoint();
            if (endpoint?.Metadata?.GetMetadata<IAllowAnonymous>() != null)
            {
                return;
            }

            // OPTIONS requests should not be blocked by permission checks
            var method = context.HttpContext.Request?.Method?.ToUpperInvariant();
            if (string.Equals(method, "OPTIONS", StringComparison.Ordinal))
            {
                return;
            }

            // TenantAdmin bypass: if current user is TenantAdmin for active tenant, skip checks
            try
            {
                var user = context.HttpContext.User;
                var userId = user?.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value
                             ?? user?.FindFirst("sub")?.Value;
                var tenantContext = context.HttpContext.RequestServices.GetService<ITenantContext>();
                var tenantId = tenantContext?.CurrentTenantId;
                if (!string.IsNullOrWhiteSpace(userId) && tenantId.HasValue && tenantId.Value != Guid.Empty)
                {
                    var cfg = context.HttpContext.RequestServices.GetService<IConfiguration>();
                    var userTenantRoleService = context.HttpContext.RequestServices.GetService<IUserTenantRoleService>();
                    var tenantAdminRoleId = cfg?.GetValue<string>("RoleScope:TenantAdminRoleId") ?? "7f3d3baf-2f5c-4f6c-9d1e-6b6d3b25a001";
                    if (userTenantRoleService != null)
                    {
                        var roles = await userTenantRoleService.GetByUserAndTenantAsync(userId, tenantId.Value);
                        var isTenantAdmin = roles != null && roles.Any(r => r?.RoleTenant?.RoleId == tenantAdminRoleId && r.IsActive && (r.RoleTenant?.IsActive ?? false));
                        if (isTenantAdmin)
                        {
                            return; // bypass permission enforcement for tenant admins
                        }
                    }
                }
            }
            catch
            {
                // Ignore and continue with normal permission check
            }

            var action = method switch
            {
                "GET" => Actions.View,
                "POST" => Actions.Create,
                "PUT" => Actions.Update,
                "PATCH" => Actions.Update,
                "DELETE" => Actions.Delete,
                _ => Actions.View
            };

            var authorizationService = context.HttpContext.RequestServices.GetRequiredService<IAuthorizationService>();
            var policyName = PermissionPolicies.Build(_resourceKey, action);
            var authResult = await authorizationService.AuthorizeAsync(context.HttpContext.User, resource: null, policyName);
            if (!authResult.Succeeded)
            {
                context.Result = new ForbidResult();
            }
        }
    }
}


