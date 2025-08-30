using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vesa.core.DTOs.Clients;
using vesa.core.Models;

namespace vesa.core.Services
{
    public interface IClientService: IService<MainClient>
    {
        Task<List<MainClientListDto>> GetByClientName(string clientName);
        Task<ClientReturnGuidId> GetClientReturnGuidId(string clientName);
        Task<Dictionary<Guid, int>> GetUserCountsByTenantAsync();
    }
}
