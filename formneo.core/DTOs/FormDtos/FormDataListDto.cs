using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace formneo.core.DTOs.FormDto
{
    public class FormDataListDto
    {
        public string id { get; set; }
        public string? FormName { get; set; }
        public string? FormDescription { get; set; }
        public int Revision { get; set; }
        public string FormDesign { get; set; }

        public int IsActive { get; set; }
    }
}
