using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vesa.core.DTOs.Budget.SF
{
    public class cust_companyGroupDto :IGenericListDto
    {
        public int Count { get; set; }

        public List<cust_companyGroupList> cust_companyGroupList { get; set; }

    }
    public class cust_companyGroupList
    {
        [JsonProperty("externalName_defaultValue")]
        public string name { get; set; }
        public string externalCode { get; set; }

    }
}
