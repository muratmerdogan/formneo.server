using System.ComponentModel.DataAnnotations.Schema;
using vesa.core.Models;
namespace vesa.core.DTOs
{
    public class WorkFlowHeadDtoWithoutItems
    {

        public string? id { get; set; }
        public string? WorkflowName { get; set; }
        public string? CurrentNodeId { get; set; }
        public string? CurrentNodeName { get; set; }
        public WorkflowStatus? workFlowStatus { get; set; }

        public string CreateUser { get; set; }

        //public virtual List<WorkflowItem>? workflowItems { get; set; }

        [ForeignKey("WorkFlowDefination")]
        public Guid WorkFlowDefinationId { get; set; }

        public virtual WorkFlowDefination WorkFlowDefination { get; set; }
        public DateTime CreatedDate { get; set; }

        public string? WorkFlowInfo { get; set; }
        
        public int UniqNumber { get; set; }
    }
}
