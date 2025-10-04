using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace formneo.core.DTOs
{
   public class WorkCompanySystemInfoUpdateDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public Guid? WorkCompanyId { get; set; }
    }
}
