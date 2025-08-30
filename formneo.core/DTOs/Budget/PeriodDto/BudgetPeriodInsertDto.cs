using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vesa.core.Models;

namespace vesa.core.DTOs.Clients
{
    public class BudgetPeriodInsertDto
    {
        public string PeriodCode { get; set; }

        public string Name { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EnDate { get; set; }

        public string Note { get; set; }


    }
}
