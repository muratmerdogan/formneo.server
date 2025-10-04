using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using formneo.core.Models.PCTracking;

namespace formneo.core.DTOs.PCTrack
{
    public class GetLastProcessTypeAndDateDto
    {
        public ProcessTypes LastProcessType { get; set; }
        public DateTime LastDateTime { get; set; }
        public LoginType LastLogonType { get; set; }
    }
}
