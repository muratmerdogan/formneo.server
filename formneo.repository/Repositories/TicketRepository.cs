using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vesa.core.Models;
using vesa.core.Models.Ticket;
using vesa.core.Repositories;

namespace vesa.repository.Repositories
{
    public class TicketRepository : GenericRepository<Tickets>, ITicketRepository
    {
        public TicketRepository(AppDbContext context) : base(context)
        {
        }
    }
}
