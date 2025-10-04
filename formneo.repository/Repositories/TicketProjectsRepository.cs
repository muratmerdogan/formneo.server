using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using formneo.core.Models;
using formneo.core.Repositories;

namespace formneo.repository.Repositories
{
    public class TicketProjectsRepository : GenericRepository<TicketProjects>, ITicketProjectsRepository
    {
        public TicketProjectsRepository(AppDbContext context) : base(context)
        {
        }
    }
}
