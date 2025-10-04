using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using formneo.core.Models;

namespace formneo.core.DTOs
{
    public class DepartmentsInsertDto
    {
        //public Guid MainClientId { get; set; }
        //public Guid CompanyId { get; set; }
        //public Guid PlantId { get; set; }
        public string Code { get; set; }
        public string DepartmentText { get; set; }

    }
}
