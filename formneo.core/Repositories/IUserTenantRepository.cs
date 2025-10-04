using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using formneo.core.Models;

namespace formneo.core.Repositories
{
    public interface IUserTenantRepository : IGenericRepository<UserTenant>
    {
        Task<UserTenant> GetByUserAndTenantAsync(string userId, Guid tenantId);
        Task<List<UserTenant>> GetAllWithIncludesAsync();
        Task<List<UserTenant>> GetByTenantWithIncludesAsync(Guid tenantId);
        Task<Dictionary<Guid, int>> GetUserCountsByTenantAsync();
    }
}


