using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vesa.core.DTOs.Plants;
using vesa.core.Models;

namespace vesa.core.Services
{
    public interface IPlantService:IService<Plant>
    {
        Task<List<PlantListDto>> GetPlants();
        Task<IEnumerable<PlantListDto>> GetByCompanyIdPlants(Guid companyId);
        Task<IEnumerable<PlantListDto>> GetByCompanyNamePlants(string companyName);
        Task<PlantReturnId> GetByPlantNameReturnId(string  plantName);
        Task<IEnumerable<PlantListCompanyName>> GetPlantListByCompanyName(string companyName);
    }
}
