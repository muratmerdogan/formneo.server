using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using formneo.core.Models;

namespace formneo.core.Services
{
	public interface IUserTenantRoleService : IService<UserTenantRole>
	{
		Task<List<UserTenantRole>> GetByUserAndTenantAsync(string userId, Guid tenantId);
	}
}


