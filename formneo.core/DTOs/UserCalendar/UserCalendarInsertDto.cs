using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vesa.core.Models.Ticket;
using vesa.core.Models.TaskManagement;


namespace vesa.core.DTOs.TaskManagement
{
    public class UserCalendarInsertDto
    {
        public string? Name { get; set; }

        public string? Description { get; set; }
        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public string? CustomerRefId { get; set; }

        public string? UserAppId { get; set; }

        public string? Percentage { get; set; } = null;
        public WorkLocation? WorkLocation { get; set; }
        public bool IsAvailable { get; set; }
    }
}
