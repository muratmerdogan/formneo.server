using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using formneo.core.DTOs.Clients;
using formneo.core.Models;
using formneo.core.Repositories;
using formneo.repository;
using formneo.repository.Repositories;

namespace formneo.repository.Repositories
{
    public class ClientRepository : GenericRepository<MainClient>, IClientRepository
    {
        public ClientRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<List<MainClient>> GetActiveAsync()
        {
            return await GetAll().Where(x => x.IsActive).ToListAsync();
        }
    }
}

