using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vesa.core.DTOs.Budget.SF
{

    public class FOLocationSFDto  : IGenericListDto
    {
        public int Count { get; set; }

        public List<FOLocationList> FOLocationList { get; set; }

    }
    public class FOLocationList
    {

        public string name { get; set; }
        public string externalCode { get; set; }

    }
}
