using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using formneo.core.Models;

namespace formneo.core.Repositories
{
	public interface IUserTenantRoleRepository : IGenericRepository<UserTenantRole>
	{
		Task<List<UserTenantRole>> GetByUserAndTenantAsync(string userId, Guid tenantId);
	}
}


