using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using formneo.core.Models;

namespace formneo.core.DTOs.Budget.JobCodeRequest
{
    public class BudgetPromotionRequestListDto
    {
        public Guid Id { get; set; }
        public string EmpCode { get; set; }

        public string PositionCode { get; set; }

        public DateTime PromotionDate { get; set; }

        public string Description { get; set; }

        public Guid? WorkflowHeadId { get; set; }

        public WorkflowHead WorkflowHead { get; set; }

        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string ManagerUser { get; set; }
        public string TeamUsers { get; set; }
    }
}
