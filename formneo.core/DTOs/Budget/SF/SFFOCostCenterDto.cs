using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace formneo.core.DTOs.Budget.SF
{
    public class SFFOCostCenterDto :IGenericListDto
    {
        public int Count { get; set; }

        public List<SFFOCostCenterList> SFFOCostCenterList { get; set; }

    }
    public class SFFOCostCenterList
    {
        [JsonProperty("name")]
        public string name { get; set; }
        public string externalCode { get; set; }

    }
}
