using vesa.core.Models.Lookup;
using vesa.core.Repositories;
using vesa.repository;
using vesa.repository.Repositories;

namespace vesa.repository.Repositories
{
	public class LookupModuleRepository : GenericRepository<LookupModule>, ILookupModuleRepository
	{
		public LookupModuleRepository(AppDbContext context) : base(context)
		{
		}
	}
}


