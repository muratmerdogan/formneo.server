using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using formneo.core.DTOs.Budget.SF;

namespace formneo.core.DTOs.Budget
{
    public class FODivisionDto :IGenericListDto
    {
        public int Count { get; set; }

        public List<FODivisionList> FODivisionList { get; set; }

    }
    public class FODivisionList
    {
        [JsonProperty("name_tr_TR")]
        public string name { get; set; }
        public string externalCode { get; set; }

    }
}
