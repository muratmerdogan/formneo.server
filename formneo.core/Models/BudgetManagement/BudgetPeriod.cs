using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using formneo.core.Models.BudgetManagement;

namespace formneo.core.Models
{
    public class BudgetPeriod : BaseEntity
    {

        [Key]
        [Required]
        public string PeriodCode { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EnDate { get; set; }

        public string Note { get; set; }

        public ICollection<BudgetPeriodUser> BudgetPeriodUsers { get; set; }

     
    }


}
