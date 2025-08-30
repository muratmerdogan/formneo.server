
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vesa.core.Models;

namespace vesa.core.DTOs.Clients
{
    public class BudgetPeriodListDto
    {
        public Guid Id { get; set; }
        public string PeriodCode { get; set; }

        public string Name { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EnDate { get; set; }

        public string Note { get; set; }

        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

    }
}
