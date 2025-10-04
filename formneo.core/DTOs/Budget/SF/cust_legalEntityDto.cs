using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace formneo.core.DTOs.Budget.SF
{
    public class cust_legalEntityDto :IGenericListDto
    {
        public int Count { get; set; }

        public List<cust_legalEntityList> cust_legalEntityList { get; set; }

    }
    public class cust_legalEntityList
    {
        [JsonProperty("externalName_defaultValue")]
        public string name { get; set; }
        public string externalCode { get; set; }

    }
}
