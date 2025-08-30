using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vesa.core.DTOs.UserCalendar
{
    public class LeaveRequestDto
    {
        public List<string> Mails { get; set; }
        public string Begda { get; set; }
        public string Endda { get; set; }
    }

}
