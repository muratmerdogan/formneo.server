using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vesa.core.DTOs.Budget.SF
{

    public class FOJobFunctionSFDto : IGenericListDto
    {
        public int Count { get; set; }

        public List<FOJobFunctionList> FOJobFunctionList { get; set; }

    }
    public class FOJobFunctionList
    {
        [JsonProperty("name")]
        public string name { get; set; }
        public string externalCode { get; set; }

    }
    public class FOJobReqGOPositionList
    {
        public string jobReqMultiSelectId { get; set; }
        public string jobReqId { get; set; }
        public string fieldName { get; set; }
        public string isPrimary { get; set; }
        public LinkApi value { get; set; }

    }
    public class FOJobRequisitionList
    {
        public string jobReqId { get; set; }
        public LinkApi status { get; set; }

    }
     public class FOJobRequisition
    {
        public string id { get; set; } 

    }
    public class LinkApi
    {
        public Defered __deferred { get; set; }
    }

    public class Defered
    {
        public string uri { get; set; }
    }
}
