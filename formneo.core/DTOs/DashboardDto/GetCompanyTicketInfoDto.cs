using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vesa.core.DTOs.DashboardDto
{
    public class GetCompanyTicketInfoDto
    {
        public string CompanyName { get; set; }
        public int TicketCount { get; set; }
        public int ResolvedCount { get; set; }
        public int OpenCount { get; set; }
    }
}
