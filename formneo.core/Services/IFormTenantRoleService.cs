using System.Collections.Generic;
using System.Threading.Tasks;
using formneo.core.DTOs.RoleForm;
using formneo.core.Models;
using NLayer.Core.Services;

namespace formneo.core.Services
{
    public interface IFormTenantRoleService : IServiceWithDto<FormTenantRole, FormTenantRoleListDto>
    {
        Task<IEnumerable<FormTenantRoleListDto>> GetAllAsync();
    }
}


