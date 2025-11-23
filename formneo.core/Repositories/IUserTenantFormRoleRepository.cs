using System.Collections.Generic;
using System.Threading.Tasks;
using formneo.core.Models;

namespace formneo.core.Repositories
{
    public interface IUserTenantFormRoleRepository : IGenericRepository<UserTenantFormRole>
    {
        Task<List<UserTenantFormRole>> GetByUserAsync(string userId);
    }
}



