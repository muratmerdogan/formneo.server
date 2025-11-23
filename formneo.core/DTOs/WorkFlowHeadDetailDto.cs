using System;
using System.Collections.Generic;
using formneo.core.Models;

namespace formneo.core.DTOs
{
    /// <summary>
    /// Workflow detail sayfası için kullanılan DTO
    /// </summary>
    public class WorkFlowHeadDetailDto
    {
        public string Id { get; set; }
        public string? WorkflowName { get; set; }
        public string WorkFlowInfo { get; set; }
        public WorkflowStatus? WorkFlowStatus { get; set; }
        public string CreateUser { get; set; }
        public int UniqNumber { get; set; }
        public Guid WorkFlowDefinationId { get; set; }
        public List<WorkFlowItemDtoWithApproveItems>? WorkflowItems { get; set; }
        /// <summary>
        /// Workflow definition JSON (nodes, edges vb. içerir)
        /// </summary>
        public string? WorkFlowDefinationJson { get; set; }
    }
}

