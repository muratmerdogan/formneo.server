using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using formneo.core.Models;
using formneo.core.Repositories;

namespace formneo.repository.Repositories
{
	public class RoleTenantRepository : GenericRepository<RoleTenant>, IRoleTenantRepository
	{
		private readonly AppDbContext _context;

		public RoleTenantRepository(AppDbContext context) : base(context)
		{
			_context = context;
		}

		public async Task<RoleTenant> GetByRoleAndTenantAsync(string roleId, Guid tenantId)
		{
			return await _context.Set<RoleTenant>()
				.AsNoTracking()
				.Include(x => x.Role)
				.Include(x => x.Tenant)
				.FirstOrDefaultAsync(x => x.RoleId == roleId && x.TenantId == tenantId);
		}

		public async Task<List<RoleTenant>> GetAllWithIncludesAsync()
		{
			return await _context.Set<RoleTenant>()
				.AsNoTracking()
				.Include(x => x.Role)
				.Include(x => x.Tenant)
				.ToListAsync();
		}

		public async Task<List<RoleTenant>> GetByTenantWithIncludesAsync(Guid tenantId)
		{
			return await _context.Set<RoleTenant>()
				.AsNoTracking()
				.Include(x => x.Role)
				.Include(x => x.Tenant)
				.Where(x => x.TenantId == tenantId)
				.ToListAsync();
		}
	}
}



