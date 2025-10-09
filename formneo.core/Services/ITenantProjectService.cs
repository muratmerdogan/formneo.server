using formneo.core.DTOs.TenantProject;
using formneo.core.Models;

namespace formneo.core.Services
{
	public interface ITenantProjectService : IService<TenantProject>
	{
		Task<IEnumerable<TenantProjectListDto>> GetAllAsyncList();
		Task<TenantProjectListDto?> GetDetailAsync(Guid id);
		Task<TenantProjectListDto> CreateAsync(TenantProjectInsertDto dto);
		Task<TenantProjectListDto?> UpdateAsync(TenantProjectUpdateDto dto);
		Task<bool> DeleteAsync(Guid id);
	}
}


