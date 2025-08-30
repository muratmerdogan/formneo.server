using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vesa.core.DTOs.Departments
{
    public class DepartmanFilterDto
    {
        public Guid? ClientId { get; set; }
        public Guid? CompanyId { get; set; }
        public Guid? PlantId { get; set; }
    }
}
