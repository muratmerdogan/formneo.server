using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vesa.core.Models
{

    public enum ApproverStatus
    {
        Pending,
        Approve,
        Reject,
        Send
    }

    public class ApproveItems : BaseEntity
    {
        [ForeignKey("WorkFlowItem")]
        public Guid WorkflowItemId { get; set; }

        public string? ApproveUser { get; set; }
        public string? ApproveUserNameSurname { get; set; }

        public string? ApprovedUser_Runtime { get; set; }

        public string? ApprovedUser_RuntimeNameSurname { get; set; }

        public string? ApprovedUser_RuntimeNote { get; set; }
        public string? ApprovedUser_RuntimeNumberManDay { get; set; }

        public string WorkFlowDescription { get; set; }

        public ApproverStatus ApproverStatus { get; set; }
        public virtual WorkflowItem WorkflowItem { get; set; }
    }
}
