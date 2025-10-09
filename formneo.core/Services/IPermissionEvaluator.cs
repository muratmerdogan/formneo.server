using System;
using System.Collections.Generic;
using formneo.core.Models.Security;

namespace formneo.core.Services
{
    public interface IPermissionEvaluator
    {
        bool Has(string userId, Guid? tenantId, string resourceKey, Actions action);
        int GetEffectiveMask(string userId, Guid? tenantId, string resourceKey);
        IDictionary<string, int> GetAllEffectiveMasks(string userId, Guid? tenantId);
    }
}


