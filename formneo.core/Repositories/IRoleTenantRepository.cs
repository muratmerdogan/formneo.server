using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using vesa.core.Models;

namespace vesa.core.Repositories
{
	public interface IRoleTenantRepository : IGenericRepository<RoleTenant>
	{
		Task<RoleTenant> GetByRoleAndTenantAsync(string roleId, Guid tenantId);
		Task<List<RoleTenant>> GetByTenantWithIncludesAsync(Guid tenantId);
		Task<List<RoleTenant>> GetAllWithIncludesAsync();
	}
}



