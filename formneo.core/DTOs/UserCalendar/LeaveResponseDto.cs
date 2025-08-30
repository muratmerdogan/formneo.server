using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vesa.core.DTOs.UserCalendar
{
    public class LeaveResponseDto
    {
        public string Mail { get; set; }
        public string Pernr { get; set; }
        public string Vorna { get; set; }
        public string Nachn { get; set; }
        public string Begda { get; set; }
        public string Endda { get; set; }
        public string Atext { get; set; }
        public string Abwtg { get; set; }
        public string Status { get; set; }
        public string DayOfWeek { get; set; }
    }
}
