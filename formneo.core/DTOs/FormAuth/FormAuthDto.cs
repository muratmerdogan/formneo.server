using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vesa.core.Models;

namespace vesa.core.DTOs.FormAuth
{
    public class FormAuthDto
    {
        public Guid Id { get; set; }

        public Guid FormId { get; set; }
        public Form Form { get; set; }

        public List<Guid>? UserIds { get; set; }

        public virtual List<UserAppDto>? Users { get; set; }
    }
}
