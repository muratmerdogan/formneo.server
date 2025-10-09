using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using formneo.repository;
using formneo.core.Models.Security;
using formneo.core.Services;

namespace formneo.api.Helper
{
    public sealed record PermissionRequirement(string ResourceKey, Actions Action) : IAuthorizationRequirement;

    public sealed class PermissionHandler : AuthorizationHandler<PermissionRequirement>
    {
        private readonly IPermissionEvaluator _evaluator;
        private readonly ITenantContext _tenantContext;
        private readonly AppDbContext _db;
        public PermissionHandler(IPermissionEvaluator evaluator, ITenantContext tenantContext, AppDbContext db)
        {
            _evaluator = evaluator;
            _tenantContext = tenantContext;
            _db = db;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {
            var userId = context.User.FindFirstValue(ClaimTypes.NameIdentifier) ?? context.User.FindFirstValue("sub");
            // Fallback: claim yoksa veya bu id Users tablosunda yoksa, Name (username/email) ile eşle
            if (string.IsNullOrWhiteSpace(userId))
            {
                var name = context.User.FindFirstValue(ClaimTypes.Name) ?? context.User.Identity?.Name;
                if (!string.IsNullOrWhiteSpace(name))
                {
                    var user = _db.Users.AsNoTracking().FirstOrDefault(u => u.UserName == name || u.Email == name);
                    if (user != null)
                    {
                        userId = user.Id;
                    }
                }
            }
            else
            {
                // Eğer claim'den gelen id AspNetUsers'ta bulunamazsa, Name ile id'yi çözmeyi dene
                var exists = _db.Users.AsNoTracking().Any(u => u.Id == userId);
                if (!exists)
                {
                    var name = context.User.FindFirstValue(ClaimTypes.Name) ?? context.User.Identity?.Name;
                    if (!string.IsNullOrWhiteSpace(name))
                    {
                        var user = _db.Users.AsNoTracking().FirstOrDefault(u => u.UserName == name || u.Email == name);
                        if (user != null)
                        {
                            userId = user.Id;
                        }
                    }
                }
            }
            var tenantId = _tenantContext?.CurrentTenantId;
            if (!string.IsNullOrEmpty(userId))
            {
                if (_evaluator.Has(userId, tenantId, requirement.ResourceKey, requirement.Action))
                {
                    context.Succeed(requirement);
                }
            }
            return Task.CompletedTask;
        }
    }

    public sealed class PermissionPolicyProvider : IAuthorizationPolicyProvider
    {
        private readonly DefaultAuthorizationPolicyProvider _fallback;
        public PermissionPolicyProvider(IServiceProvider services)
        {
            _fallback = new DefaultAuthorizationPolicyProvider(services.GetRequiredService<Microsoft.Extensions.Options.IOptions<AuthorizationOptions>>());
        }

        public Task<AuthorizationPolicy> GetDefaultPolicyAsync() => _fallback.GetDefaultPolicyAsync();
        public Task<AuthorizationPolicy?> GetFallbackPolicyAsync() => _fallback.GetFallbackPolicyAsync();

        public Task<AuthorizationPolicy> GetPolicyAsync(string policyName)
        {
            if (policyName != null && policyName.StartsWith("perm:"))
            {
                // Format: perm:ResourceKey:maskInt
                var parts = policyName.Split(':');
                if (parts.Length == 3 && int.TryParse(parts[2], out var mask))
                {
                    var action = (Actions)mask;
                    var resource = parts[1];
                    var policy = new AuthorizationPolicyBuilder()
                        .AddRequirements(new PermissionRequirement(resource, action))
                        .Build();
                    return Task.FromResult(policy);
                }
            }
            return _fallback.GetPolicyAsync(policyName);
        }
    }
}


