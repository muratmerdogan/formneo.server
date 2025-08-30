using System.ComponentModel.DataAnnotations.Schema;
using vesa.core.Models;

namespace vesa.core.DTOs
{
    public class WorkCompanyTicketMatrisListDto
    {
        public Guid Id { get; set; }

        public Guid FromCompanyId { get; set; }
        public WorkCompanyDto FromCompany { get; set; }

        public List<Guid>? ToCompaniesIds { get; set; }

        public virtual List<WorkCompanyDto>? ToCompanies { get; set; }
    }
}
