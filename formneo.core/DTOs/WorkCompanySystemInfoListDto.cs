using System.ComponentModel.DataAnnotations.Schema;
using formneo.core.Models;

namespace formneo.core.DTOs
{
    public class WorkCompanySystemInfoListDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        [ForeignKey("WorkCompany")]
        public Guid? WorkCompanyId { get; set; }

        public virtual WorkCompany? WorkCompany { get; set; }
    }
}
