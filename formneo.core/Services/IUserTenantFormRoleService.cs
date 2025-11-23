using System.Collections.Generic;
using System.Threading.Tasks;
using formneo.core.DTOs.RoleForm;

namespace formneo.core.Services
{
    public interface IUserTenantFormRoleService
    {
        Task<List<UserTenantFormRoleListDto>> GetByUserAsync(string userId);
        Task BulkSaveAsync(UserTenantFormRoleBulkSaveDto dto);
    }
}



