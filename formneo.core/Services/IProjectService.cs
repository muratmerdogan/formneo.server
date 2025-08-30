using vesa.core.DTOs.ProjectDtos;
using vesa.core.Models;

namespace vesa.core.Services
{
    public interface IProjectService : IService<Project>
    {
        Task<IEnumerable<GetProjectListDto>> GetAllProductListAsync();
        Task<IEnumerable<GetProjectListDto>> GetByUserProductListAsync(string userId);
    }

}
