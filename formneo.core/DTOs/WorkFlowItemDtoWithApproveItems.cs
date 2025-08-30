using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vesa.core.DTOs;
using vesa.core.Models;

namespace vesa.core
{
    public class WorkFlowItemDtoWithApproveItems
    {
  
        public string NodeId { get; set; }

        public string NodeName { get; set; }

        public string NodeType { get; set; }

        public string NodeDescription { get; set; }

        public WorkflowStatus workFlowNodeStatus { get; set; }

        public virtual List<ApproveItemsDto>? approveItems { get; set; }
    }
}
