using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using formneo.core.DTOs.Plants;
using formneo.core.Models;

namespace formneo.core.Services
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
