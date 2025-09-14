using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NLayer.Core.Services;
using vesa.core.DTOs;
using vesa.core.Models;

namespace vesa.core.Services
{
    public interface IRoleTenantMenuService : IGlobalServiceWithDto<AspNetRolesTenantMenu, RoleTenantMenuListDto>
    {
        Task BulkSaveAsync(RoleTenantMenuBulkSaveDto dto);
        Task<IEnumerable<RoleTenantMenuListDto>> GetByRoleAndTenantAsync(string roleId, Guid tenantId);
        Task RemoveByRoleAndTenantAsync(string roleId, Guid tenantId);
    }
}


