using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using formneo.core.Models;
using formneo.core.Models.Ticket;
using formneo.core.Repositories;

namespace formneo.repository.Repositories
{
    public class TicketRepository : GenericRepository<Tickets>, ITicketRepository
    {
        public TicketRepository(AppDbContext context) : base(context)
        {
        }
    }
}
