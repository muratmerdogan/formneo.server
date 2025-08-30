using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vesa.core.Models;

namespace vesa.core.DTOs
{
    public class EmpSalaryDto
    {

        public string PersId { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public decimal? Salary { get; set; }

        public string Guid { get; set; }

    }
}
