using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using formneo.core.Models.BudgetManagement;

namespace formneo.core.Models
{

    public enum WorkflowStatus
    {
        NotStarted,
        InProgress,
        Completed,
        Pending,
        SendBack,
        // Diğer durumlar eklenabilir...
    }

    public class WorkflowHead : BaseEntity
    {

         public string? WorkflowName { get; set; }

        public string? CurrentNodeId { get; set; }

        public string? CurrentNodeName { get; set; }

        public WorkflowStatus? workFlowStatus { get; set; }

        public string? WorkFlowInfo { get; set; }
        public string CreateUser { get; set; }

        public virtual List<WorkflowItem>? workflowItems { get; set; }


        [ForeignKey("WorkFlowDefination")]
        public Guid WorkFlowDefinationId { get; set; }

        public virtual WorkFlowDefination WorkFlowDefination { get; set; }

        public string WorkFlowDefinationJson { get; set; }




    }





}
