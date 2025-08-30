using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vesa.core.DTOs;
using vesa.core.DTOs.Departments;
using vesa.core.Models;

namespace vesa.core.Services
{

    public interface IDepartmentService : IService<Departments>
    {
        Task<List<DepartmentListAllNameDto>> GetAllClientCompanyPlantNames();

        Task<List<DepartmentListAllNameDto>> GetFilterClientCompanyPlantName(DepartmanFilterDto dto);
        Task<IEnumerable<DepartmentsListDto>> GetDepermantListAsync();
    }
    
}
