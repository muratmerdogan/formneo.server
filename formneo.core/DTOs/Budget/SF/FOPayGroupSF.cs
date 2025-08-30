using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vesa.core.DTOs.Budget.SF
{

    public class FOPayGroupSFDto :IGenericListDto
    {
        public int Count { get; set; }

        public List<FOPayGroupSFList> FOPayGroupSFList { get; set; }

    }
    public class FOPayGroupSFList
    {

        [JsonProperty("name_tr_TR")]
        public string name { get; set; }
        public string externalCode { get; set; }

    }
}
