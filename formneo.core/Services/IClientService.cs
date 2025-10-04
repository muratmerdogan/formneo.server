using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using formneo.core.DTOs.Clients;
using formneo.core.Models;

namespace formneo.core.Services
{
    public interface IClientService: IService<MainClient>
    {
        Task<List<MainClientListDto>> GetByClientName(string clientName);
        Task<ClientReturnGuidId> GetClientReturnGuidId(string clientName);
        Task<Dictionary<Guid, int>> GetUserCountsByTenantAsync();
    }
}
