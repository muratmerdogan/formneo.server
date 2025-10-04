using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace formneo.core.DTOs.FormAuth
{
    public class FormAuthUpdateDto
    {
        public Guid FormId { get; set; }
        public List<Guid>? UserIds { get; set; }
    }
}
