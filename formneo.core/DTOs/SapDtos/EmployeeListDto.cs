using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace formneo.core.DTOs.SapDtos
{
    public class EmployeeDto
    {


        public string ENAME { get; set; }
        public string Photo { get; set; }
        public int PERNR { get; set; } // PERNR
        public string EMAIL { get; set; } // ENAME

        [JsonProperty("STEXT")]
        public string STEXT { get; set; }
    }
}
