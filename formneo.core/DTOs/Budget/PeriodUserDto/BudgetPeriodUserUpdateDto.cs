using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vesa.core.Models.BudgetManagement;

namespace vesa.core.DTOs.Budget.PeriodUserDto
{
    public class BudgetPeriodUserUpdateDto
    {
        public Guid Id { get; set; }

        [Required]
        public string BudgetPeriodCode { get; set; }

        public string UserName { get; set; }

        public RequestType requestType { get; set; }

        public Permission permission { get; set; }

        public ProcessType processType { get; set; }

    }
}
