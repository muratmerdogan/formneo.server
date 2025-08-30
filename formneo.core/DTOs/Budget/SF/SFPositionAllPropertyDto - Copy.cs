using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vesa.core.DTOs.Budget.SF
{
    public class SFEmpJobDto
    {

        [JsonProperty("name")]
        public string name { get; set; }


        [JsonProperty("code")]
        public string code { get; set; }

        [JsonProperty("userId")]
        public string userId { get; set; }

        [JsonProperty("managerId")]
        public string? managerId { get; set; }


    }
}
