using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using formneo.core.Models;

namespace formneo.core.DTOs.Budget.JobCodeRequest
{
    public class BudgetPromotionRequestInsertDto
    {

        public string EmpCode { get; set; }

        public string PositionCode { get; set; }

        public DateTime PromotionDate { get; set; }

        public string Description { get; set; }
        public string ManagerUser { get; set; }
        public string TeamUsers { get; set; }



    }
}
