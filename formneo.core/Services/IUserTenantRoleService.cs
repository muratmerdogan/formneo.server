using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using vesa.core.Models;

namespace vesa.core.Services
{
	public interface IUserTenantRoleService : IService<UserTenantRole>
	{
		Task<List<UserTenantRole>> GetByUserAndTenantAsync(string userId, Guid tenantId);
	}
}


