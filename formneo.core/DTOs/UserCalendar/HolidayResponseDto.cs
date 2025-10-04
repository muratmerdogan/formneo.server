using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace formneo.core.DTOs.UserCalendar
{
    public class HolidayResponseDto
    {
        public string Tarih { get; set; }
        public string Resmi_Tatil { get; set; }
        public string DayOfWeek { get; set; }

    }
}
