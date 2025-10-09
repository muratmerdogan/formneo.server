using Microsoft.AspNetCore.Authorization;
using formneo.core.Models.Security;

namespace formneo.api.Helper
{
    public sealed class RequirePermissionAttribute : AuthorizeAttribute
    {
        public RequirePermissionAttribute(string resourceKey, Actions action)
        {
            Policy = PermissionPolicies.Build(resourceKey, action);
        }
    }
}


