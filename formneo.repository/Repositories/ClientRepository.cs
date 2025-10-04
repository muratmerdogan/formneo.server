using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using formneo.core.Models;
using formneo.core.Repositories;

namespace formneo.repository.Repositories
{
    public class ClientRepository : GenericRepository<MainClient>, IClientRepository
    {

        private readonly AppDbContext _appDbContext;
        public ClientRepository(AppDbContext context) : base(context)
        {
        }
    }
}
