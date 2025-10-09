using formneo.core.Models;
using formneo.core.Repositories;

namespace formneo.repository.Repositories
{
	public class ProjectTeamMemberRepository : GenericRepository<ProjectTeamMember>, IProjectTeamMemberRepository
	{
		public ProjectTeamMemberRepository(AppDbContext context) : base(context)
		{
		}
	}
}


