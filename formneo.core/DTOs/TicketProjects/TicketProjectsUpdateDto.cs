using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using formneo.core.Models;

namespace formneo.core.DTOs.TicketProjects
{
    public class TicketProjectsUpdateDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? SubProjectName { get; set; }
        public string? Description { get; set; }
        public string? Risks { get; set; }
        public string? ReportsUrl { get; set; }
        public bool? IsActive { get; set; }
        public Guid? WorkCompanyId { get; set; }
        // proje sorumlusu
        [ForeignKey("UserApp")]
        public string? ManagerId { get; set; }

        // proje çalışanları
        public List<string>? UserIds { get; set; }
        public string? UserIdsJoin { get; set; }
        public Guid? ProjectCategoryId { get; set; }
        
    }
}
