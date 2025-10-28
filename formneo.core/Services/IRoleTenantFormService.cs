using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NLayer.Core.Services;
using formneo.core.DTOs.RoleForm;
using formneo.core.Models;

namespace formneo.core.Services
{
    public interface IRoleTenantFormService : IServiceWithDto<AspNetRolesTenantForm, RoleTenantFormListDto>
    {
        Task<IEnumerable<RoleTenantFormListDto>> GetAllAsync();
        Task<RoleTenantFormDetailDto> GetDetailByFormTenantRoleAsync(Guid formTenantRoleId);
        Task<Guid> InsertAsync(RoleTenantFormInsertDto dto);
        Task<Guid> UpdateAsync(RoleTenantFormUpdateDto dto);
    }
}


