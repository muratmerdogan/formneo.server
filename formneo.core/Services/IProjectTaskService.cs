using formneo.core.DTOs.ProjectTask;
using formneo.core.Models;

namespace formneo.core.Services
{
	public interface IProjectTaskService : IService<ProjectTask>
	{
		Task<IEnumerable<ProjectTaskListDto>> GetByProjectAsync(Guid projectId);
		Task<ProjectTaskListDto?> GetDetailAsync(Guid id);
		Task<ProjectTaskListDto> CreateAsync(ProjectTaskInsertDto dto);
		Task<ProjectTaskListDto?> UpdateAsync(ProjectTaskUpdateDto dto);
		Task<bool> DeleteAsync(Guid id);
	}
}


