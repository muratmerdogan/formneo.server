using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using formneo.core.Models.Ticket;

namespace formneo.core.DTOs.DashboardDto
{
    public class GetTicketCustomerAssignGroup
    {
        public string Name { get; set; }

        public TicketStatus  status { get; set; }

   


    }

    public class GetTicketCustomerAssignGroupGroup
    {
        public string Name { get; set; }

        public int TotalCount { get; set; }

        public int OpenCount { get; set; }


        public int UnitTest { get; set; }
        public int CustomerTest { get; set; }

    }
}
