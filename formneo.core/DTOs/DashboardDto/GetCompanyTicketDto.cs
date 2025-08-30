using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vesa.core.DTOs.DashboardDto
{
    public class GetCompanyTicketDto
    {
        public string CompanyName { get; set; }
        public int TicketCount { get; set; }
    }
}
