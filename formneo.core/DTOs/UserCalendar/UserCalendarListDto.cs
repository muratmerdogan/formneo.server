using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using formneo.core.Models;
using formneo.core.Models.TaskManagement;

namespace formneo.core.DTOs.TaskManagement
{
    public class UserCalendarListDto
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }

        public string? Description { get; set; }
        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public string? CustomerRefId { get; set; } = null;
        public WorkCompany? CustomerRef { get; set; } = null;

        public string? UserAppId { get; set; }
        public UserAppDtoWithoutPhoto? UserAppDtoWithoutPhoto { get; set; }
        public UserAppDto? UserAppDto { get; set; }
        public string? Color { get; set; } = null;
        public string? Percentage { get; set; } = null;
        public WorkLocation? WorkLocation { get; set; }
        public List<bool>? DaysOfWeek { get; set; } // ["Mon", "Tue", "Wed", "Thu", "", "", ""]
        public bool IsAvailable { get; set; }
    }
}
