using formneo.core.DTOs.ProjectDtos;
using formneo.core.Models;

namespace formneo.core.Services
{
    public interface IProjectService : IService<Project>
    {
        Task<IEnumerable<GetProjectListDto>> GetAllProductListAsync();
        Task<IEnumerable<GetProjectListDto>> GetByUserProductListAsync(string userId);
    }

}
