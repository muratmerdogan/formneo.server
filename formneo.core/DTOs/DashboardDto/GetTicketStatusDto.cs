using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vesa.core.DTOs.DashboardDto
{
    public class GetTicketStatusDto
    {
        public int OpenCount { get; set; }
        public int ClosedCount { get; set; }
    }
}
