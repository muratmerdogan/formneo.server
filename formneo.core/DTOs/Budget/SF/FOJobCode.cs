using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace formneo.core.DTOs.Budget.SF
{
    public class FOJobCodeDto :IGenericListDto
    {
        public int Count { get; set; }

        public List<FOJobCodeList> FOJobCodeList { get; set; }

    }
    public class FOJobCodeList
    {

        public string name { get; set; }
        public string externalCode { get; set; }

        public string cust_ronesanskademe { get; set; }

        public string cust_joblevelgroup { get; set; }

        public string grade { get; set; }

        public string name_ru_RU { get; set; }
        public string name_tr_TR { get; set; }
        public string name_en_US { get; set; }
        public string cust_haykademe { get; set; }

        public string employeeClass { get; set; }
        public string jobFunction { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }

    }
}
