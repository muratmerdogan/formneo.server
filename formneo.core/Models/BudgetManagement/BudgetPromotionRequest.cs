using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vesa.core.Models.BudgetManagement
{
    public class BudgetPromotionRequest : BaseEntity
    {
        public string EmpCode { get; set; }

        public string PositionCode { get; set; }

        public DateTime PromotionDate { get; set; }

        public string Description { get; set; }

        [ForeignKey("WorkflowHead")]
        public Guid? WorkflowHeadId { get; set; }
        public virtual WorkflowHead? WorkflowHead { get; set; }
        public Boolean IsSend { get; set; }
        public string ManagerUser { get; set; }
        public string TeamUsers { get; set; }

    }
}
