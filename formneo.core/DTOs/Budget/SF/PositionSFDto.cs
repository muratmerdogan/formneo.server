using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vesa.core.DTOs.Budget.SF
{

    public class PositionSFDto : IGenericListDto
    {
        public int Count { get; set; }

        public List<SFPositionList> SFPositionList { get; set; }

    }
    public class SFPositionList
    {

        //public string division { get; set; }
        //public string externalName_defaultValue { get; set; }

        //public string positionTitle { get; set; }


        [JsonProperty("externalName_defaultValue")]
        public string name { get; set; }


        [JsonProperty("code")]
        public string externalCode { get; set; }


        public bool vacant { get; set; }


        public string userId { get; set; }
        public string userName { get; set; }
        public string cust_plannedEndDate { get; set; }
        public string cust_IseBaslamaTarihi { get; set; }
        public string businessUnit { get; set; }
        public string cust_customlegalEntity { get; set; }
        public string cust_sub_division { get; set; }
        public string department { get; set; }

    }


    public class SFEmpJob
    {
        public string userId { get; set; }
        public string managerId { get; set; }

        public string displayName { get; set; }
    }

    public class SFUsers
    {
        public string userId { get; set; }
        public string displayName { get; set; }
    }

    
}
