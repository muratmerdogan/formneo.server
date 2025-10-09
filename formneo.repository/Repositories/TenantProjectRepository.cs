using formneo.core.Models;
using formneo.core.Repositories;

namespace formneo.repository.Repositories
{
	public class TenantProjectRepository : GenericRepository<TenantProject>, ITenantProjectRepository
	{
		public TenantProjectRepository(AppDbContext context) : base(context)
		{
		}
	}
}


