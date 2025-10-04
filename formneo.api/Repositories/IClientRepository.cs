using System.Collections.Generic;
using System.Threading.Tasks;
using formneo.core.DTOs.Clients;
using formneo.core.Models;
using formneo.core.Repositories;

namespace formneo.core.Repositories
{
    public interface IClientRepository : IGenericRepository<MainClient>
    {
        Task<List<MainClient>> GetActiveAsync();
    }
}

