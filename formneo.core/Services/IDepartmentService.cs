using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using formneo.core.DTOs;
using formneo.core.DTOs.Departments;
using formneo.core.Models;

namespace formneo.core.Services
{

    public interface IDepartmentService : IService<Departments>
    {
        Task<List<DepartmentListAllNameDto>> GetAllClientCompanyPlantNames();

        Task<List<DepartmentListAllNameDto>> GetFilterClientCompanyPlantName(DepartmanFilterDto dto);
        Task<IEnumerable<DepartmentsListDto>> GetDepermantListAsync();
    }
    
}
