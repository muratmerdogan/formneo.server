using System;
using Microsoft.AspNetCore.Authorization;
using formneo.core.Models.Security;

namespace formneo.api.Helper
{
    public static class PermissionPolicies
    {
        public static string Build(string resourceKey, Actions action)
            => $"perm:{resourceKey}:{(int)action}";
    }
}


