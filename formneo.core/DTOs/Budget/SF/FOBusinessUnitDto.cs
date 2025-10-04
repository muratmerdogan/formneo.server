using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace formneo.core.DTOs.Budget.SF
{
    public class FOBusinessUnitDto :IGenericListDto
    {
        public int Count { get; set; }

        public List<FOBusinessUnitList> FOBusinessUnitList { get; set; }

    }
    public class FOBusinessUnitList
    {
        [JsonProperty("name")]
        public string name { get; set; }
        public string externalCode { get; set; }

    }
}
