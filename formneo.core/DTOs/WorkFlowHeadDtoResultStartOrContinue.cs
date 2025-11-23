using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using formneo.core.Models;

using formneo.core.Models;

namespace formneo.core.DTOs
{
    public class WorkFlowHeadDtoResultStartOrContinue
    {
        public string Id { get; set; }

        public string? WorkFlowInfo { get; set; }
        
        /// <summary>
        /// Eğer workflow bir alertNode'a geldiyse, alert bilgileri burada döner
        /// </summary>
        public AlertNodeInfo? AlertInfo { get; set; }
        
        /// <summary>
        /// Workflow'un durumu (Pending ise kullanıcıdan aksiyon bekleniyor)
        /// </summary>
        public WorkflowStatus? WorkFlowStatus { get; set; }
        
        /// <summary>
        /// Pending durumundaki node'un ID'si (alert veya form için)
        /// </summary>
        public string? PendingNodeId { get; set; }
        
        /// <summary>
        /// FormNode completed oldu mu? (Form kapanmalı mı?)
        /// </summary>
        public bool FormNodeCompleted { get; set; }
        
        /// <summary>
        /// Completed olan formNode'un ID'si
        /// </summary>
        public string? CompletedFormNodeId { get; set; }
    }
    
    public class AlertNodeInfo
    {
        public string Title { get; set; }
        public string Message { get; set; }
        public string Type { get; set; } // info, success, warning, error
        public string NodeId { get; set; }
    }
}
