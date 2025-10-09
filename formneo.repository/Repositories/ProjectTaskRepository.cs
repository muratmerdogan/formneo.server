using formneo.core.Models;
using formneo.core.Repositories;

namespace formneo.repository.Repositories
{
	public class ProjectTaskRepository : GenericRepository<ProjectTask>, IProjectTaskRepository
	{
		public ProjectTaskRepository(AppDbContext context) : base(context)
		{
		}
	}
}


