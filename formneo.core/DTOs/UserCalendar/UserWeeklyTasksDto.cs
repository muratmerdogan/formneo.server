using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vesa.core.DTOs.TaskManagement;

namespace vesa.core.DTOs.UserCalendar
{
    public class UserWeeklyTasksDto
    {
        public Guid UserId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? TicketDepartmentId { get; set; }
        public string? PositionId { get; set; }
        public List<UserCalendarListDto>? Tasks { get; set; }
    }

}
