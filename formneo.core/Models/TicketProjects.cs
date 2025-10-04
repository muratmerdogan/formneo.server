using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using formneo.core.Models.Ticket;

namespace formneo.core.Models
{
    public class TicketProjects : BaseEntity
    {
        public string Name { get; set; }
        public string? SubProjectName { get; set; }
        public string? Description { get; set; }
        public string? Risks { get; set; }
        public string? ReportsUrl { get; set; }

        [ForeignKey("WorkCompany")]
        public Guid? WorkCompanyId { get; set; }
        public virtual WorkCompany? WorkCompany { get; set; }

        // proje sorumlusu
        [ForeignKey("UserApp")]
        public string? ManagerId { get; set; }
        public virtual UserApp? Manager { get; set; }

        // proje çalışanları
        public string? UserIds { get; set; }
        public bool? IsActive { get; set; }

        [ForeignKey("ProjectCategories")]
        public Guid? ProjectCategoryId { get; set; }
        public virtual ProjectCategories? ProjectCategory { get; set; }
    }
}
