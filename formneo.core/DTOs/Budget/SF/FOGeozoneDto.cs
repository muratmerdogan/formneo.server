using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace formneo.core.DTOs.Budget.SF
{
    public class FOGeozoneDto :IGenericListDto
    {
        public int Count { get; set; }

        public List<FOGeozoneDtoList> FOGeozoneDtoList { get; set; }

    }
    public class FOGeozoneDtoList
    {
        public string name { get; set; }
        public string externalCode { get; set; }

    }
}
