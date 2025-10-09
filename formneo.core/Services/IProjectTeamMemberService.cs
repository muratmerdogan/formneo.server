using formneo.core.DTOs.ProjectTeamMember;
using formneo.core.Models;

namespace formneo.core.Services
{
	public interface IProjectTeamMemberService : IService<ProjectTeamMember>
	{
		Task<IEnumerable<ProjectTeamMemberListDto>> GetByProjectAsync(Guid projectId);
		Task<ProjectTeamMemberListDto> CreateAsync(ProjectTeamMemberInsertDto dto);
		Task<ProjectTeamMemberListDto?> UpdateAsync(ProjectTeamMemberUpdateDto dto);
		Task<bool> DeleteAsync(Guid id);
	}
}


