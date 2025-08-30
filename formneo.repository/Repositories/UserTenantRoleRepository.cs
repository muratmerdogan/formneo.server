using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using vesa.core.Models;
using vesa.core.Repositories;

namespace vesa.repository.Repositories
{
	public class UserTenantRoleRepository : GenericRepository<UserTenantRole>, IUserTenantRoleRepository
	{
		public UserTenantRoleRepository(AppDbContext context) : base(context) { }

		public async Task<List<UserTenantRole>> GetByUserAndTenantAsync(string userId, Guid tenantId)
		{
            return await _context.Set<UserTenantRole>()
                .Include(x => x.RoleTenant)
                .Where(x => x.UserId == userId && x.RoleTenant.TenantId == tenantId && x.IsActive && x.RoleTenant.IsActive)
                .ToListAsync();
		}
	}
}


