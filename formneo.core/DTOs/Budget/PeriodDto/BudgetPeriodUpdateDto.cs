using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using formneo.core.Models;

namespace formneo.core.DTOs.Clients
{
    public class BudgetPeriodUpdateDto
    {     
        public Guid Id { get; set; }
        public string PeriodCode { get; set; }

        public string Name { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EnDate { get; set; }


        public string Note { get; set; }
    }
}
