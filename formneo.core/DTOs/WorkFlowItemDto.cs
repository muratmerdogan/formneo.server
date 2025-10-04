using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using formneo.core.Models;

namespace formneo.core.DTOs
{
    public class WorkFlowItemDto
    {
        public WorkFlowHeadDtoWithoutItems WorkflowHead { get; set; }

        public string NodeId { get; set; }

        public string NodeName { get; set; }

        public string NodeType { get; set; }

        public string NodeDescription { get; set; }

        public WorkflowStatus workFlowNodeStatus { get; set; }
    }
}
