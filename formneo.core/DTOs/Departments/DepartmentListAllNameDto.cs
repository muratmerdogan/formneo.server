using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vesa.core.DTOs.Departments
{
    public class DepartmentListAllNameDto
    {
        public Guid Id { get; set; }

        public string Code { get; set; }

        public string DepartmentText { get; set; }
        public string ClientName { get; set; }
        public string CompanyName { get; set; }
        public string PlantName { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
