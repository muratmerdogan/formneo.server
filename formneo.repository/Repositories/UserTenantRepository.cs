using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using formneo.core.Models;
using formneo.core.Repositories;

namespace formneo.repository.Repositories
{
    public class UserTenantRepository : GenericRepository<UserTenant>, IUserTenantRepository
    {
        private readonly AppDbContext _context;

        public UserTenantRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<UserTenant> GetByUserAndTenantAsync(string userId, Guid tenantId)
        {
            return await _context.Set<UserTenant>()
                .AsNoTracking()
                .Include(x => x.User)
                .Include(x => x.Tenant)
                .FirstOrDefaultAsync(x => x.UserId == userId && x.TenantId == tenantId);
        }

        public async Task<List<UserTenant>> GetAllWithIncludesAsync()
        {
            return await _context.Set<UserTenant>()
                .AsNoTracking()
                .Include(x => x.User)
                .Include(x => x.Tenant)
                .ToListAsync();
        }

        public async Task<List<UserTenant>> GetByTenantWithIncludesAsync(Guid tenantId)
        {
            return await _context.Set<UserTenant>()
                .AsNoTracking()
                .Include(x => x.User)
                .Include(x => x.Tenant)
                .Where(x => x.TenantId == tenantId)
                .ToListAsync();
        }

        public async Task<Dictionary<Guid, int>> GetUserCountsByTenantAsync()
        {
            return await _context.Set<UserTenant>()
                .AsNoTracking()
                .GroupBy(x => x.TenantId)
                .Select(g => new { TenantId = g.Key, Count = g.Count() })
                .ToDictionaryAsync(k => k.TenantId, v => v.Count);
        }
    }
}


