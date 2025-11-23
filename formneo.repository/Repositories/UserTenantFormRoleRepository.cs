using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using formneo.core.Models;
using formneo.core.Repositories;

namespace formneo.repository.Repositories
{
    public class UserTenantFormRoleRepository : GenericRepository<UserTenantFormRole>, IUserTenantFormRoleRepository
    {
        public UserTenantFormRoleRepository(AppDbContext context) : base(context) { }

        public async Task<List<UserTenantFormRole>> GetByUserAsync(string userId)
        {
            return await _context.Set<UserTenantFormRole>()
                .Include(x => x.FormTenantRole)
                .Where(x => x.UserId == userId && x.IsActive)
                .ToListAsync();
        }
    }
}



