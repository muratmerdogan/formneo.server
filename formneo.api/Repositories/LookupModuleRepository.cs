using formneo.core.Models.Lookup;
using formneo.core.Repositories;
using formneo.repository;
using formneo.repository.Repositories;

namespace formneo.repository.Repositories
{
	public class LookupModuleRepository : GenericRepository<TenantLookupModule>, ILookupModuleRepository
	{
		public LookupModuleRepository(AppDbContext context) : base(context)
		{
		}
	}
}


