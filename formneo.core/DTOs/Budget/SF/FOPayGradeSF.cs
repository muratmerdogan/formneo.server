using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace formneo.core.DTOs.Budget.SF
{

    public class FOPayGradeSFDto :IGenericListDto
    {
        public int Count { get; set; }

        public List<FOPayGradeList> FOPayGradeList { get; set; }

    }
    public class FOPayGradeList
    {

        public string name { get; set; }
        public string externalCode { get; set; }

    }
}
