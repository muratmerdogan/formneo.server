using System.Collections.Generic;
using System.Threading.Tasks;
using vesa.core.DTOs.Clients;
using vesa.core.Models;
using vesa.core.Repositories;

namespace vesa.core.Repositories
{
    public interface IClientRepository : IGenericRepository<MainClient>
    {
        Task<List<MainClient>> GetActiveAsync();
    }
}

