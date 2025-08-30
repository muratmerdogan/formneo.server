using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vesa.core.Models.BudgetManagement;
using vesa.core.Models;

namespace vesa.core.DTOs.Budget.PeriodUserDto
{
    public class BudgetPeriodUserInsertDto
    {

        [Required]
        public string BudgetPeriodCode { get; set; }

        public string UserName { get; set; }

        public RequestType requestType { get; set; }

        public Permission permission { get; set; }

        public ProcessType processType { get; set; }

        public string nameSurname { get; set; }
    }
}
