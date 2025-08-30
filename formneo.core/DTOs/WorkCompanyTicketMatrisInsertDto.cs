using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vesa.core.DTOs
{
    public class WorkCompanyTicketMatrisInsertDto
    {
        public Guid FromCompanyId { get; set; }
        public List<Guid> ToCompaniesIds { get; set; }
    }
}
