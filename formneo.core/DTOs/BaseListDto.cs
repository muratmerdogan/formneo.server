using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vesa.core.DTOs
{
    public class BaseListDto
    {
        public string MainClientId { get; set; }
        public string CompanyId { get; set; }
        public string PlantId { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; }
    }
}
