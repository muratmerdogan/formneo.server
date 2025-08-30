using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vesa.core.Models
{
    public class EmpSalary : BaseEntity
    {

        [ForeignKey("Employee")]
        public Guid EmployeeId { get; set; }

        public  virtual Employee Employee { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public decimal? Salary { get; set; }

    }
}
