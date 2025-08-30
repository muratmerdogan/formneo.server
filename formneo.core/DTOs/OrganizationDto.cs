using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vesa.core.DTOs
{
    public class OrganizationDto
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
        public string? Title { get; set; }
        public string? Email { get; set; }
        public string? Photo { get; set; }
        public string? ClassName { get; set; }
        public string? Type { get; set; }
        public bool Expanded { get; set; }
        public List<OrganizationDto>? Children { get; set; }
    }
}
