using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace formneo.core.Models
{

    public class WorkflowItem : BaseEntity
    {

        [ForeignKey("WorkflowHead")]
        public Guid WorkflowHeadId { get; set; }

        public virtual WorkflowHead WorkflowHead { get; set; }

        public string NodeId { get; set; }

        public string NodeName { get; set; }

        public string NodeType { get; set; }

        public string NodeDescription { get; set; }

        public WorkflowStatus workFlowNodeStatus { get; set; }

        public virtual List<ApproveItems>? approveItems { get; set; }

        public virtual List<FormItems>? formItems { get; set; }
    }

}
