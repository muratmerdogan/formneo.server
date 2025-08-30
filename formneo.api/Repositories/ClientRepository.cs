using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using vesa.core.DTOs.Clients;
using vesa.core.Models;
using vesa.core.Repositories;
using vesa.repository;
using vesa.repository.Repositories;

namespace vesa.repository.Repositories
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

