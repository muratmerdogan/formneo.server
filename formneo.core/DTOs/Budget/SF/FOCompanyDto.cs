using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vesa.core.DTOs.Budget.SF
{
    public class FOCompanyDto :IGenericListDto
    {
        public int Count { get; set; }

        public List<FOCompanyDtoList> FOCompanyDtoList { get; set; }

    }
    public class FOCompanyDtoList
    {
        [JsonProperty("name")]
        public string name { get; set; }
        public string externalCode { get; set; }

    }
}
