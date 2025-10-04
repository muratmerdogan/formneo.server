using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace formneo.core.DTOs
{
    public class WorkCompanyTicketMatrisUpdateDto
    {
        public Guid FromCompanyId { get; set; }
        public List<Guid> ToCompaniesIds { get; set; }
    }
}
