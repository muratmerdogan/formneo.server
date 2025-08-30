using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vesa.core.DTOs.Budget.SF;

namespace vesa.core.GenericListDto
{
    public class support_UsersGenericSelectDto : IGenericListDto
    {
        public int Count { get; set; }

        public List<support_UsersGenericList> support_UsersGenericList { get; set; }

    }
    public class support_UsersGenericList
    {

        public string name { get; set; }
        public string externalCode { get; set; }

    }
}
