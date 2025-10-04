using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace formneo.core.DTOs.Budget.SF
{
    public class FODepartmentSFDto : IGenericListDto
    {
        public int Count { get; set; }

        public List<FODepartmentList> FODepartmentList { get; set; }

    }
    public class FODepartmentList
    {
        [JsonProperty("name")]
        public string name { get; set; }
        public string externalCode { get; set; }

    }
}
