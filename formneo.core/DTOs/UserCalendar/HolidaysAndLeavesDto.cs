using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vesa.core.DTOs.UserCalendar
{
    public class HolidaysAndLeavesDto
    {
        public List<LeaveResponseDto>? leaves {  get; set; }
        public List<HolidayResponseDto>? holidays { get; set; }
    }
}
