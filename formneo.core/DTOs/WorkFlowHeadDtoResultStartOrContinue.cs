using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using formneo.core.Models;

namespace formneo.core.DTOs
{
    public class WorkFlowHeadDtoResultStartOrContinue
    {
        public string Id { get; set; }

        public string? WorkFlowInfo { get; set; }
        
        /// <summary>
        /// Workflow'un durumu (Pending ise kullanıcıdan aksiyon bekleniyor)
        /// </summary>
        public WorkflowStatus? WorkFlowStatus { get; set; }
        
        /// <summary>
        /// Pending durumundaki node'un ID'si (alert, form veya approver için)
        /// </summary>
        public string? PendingNodeId { get; set; }
        
        /// <summary>
        /// Mevcut node'un detaylı bilgileri (generic yapı - her node tipi için özel bilgiler içerir)
        /// </summary>
        public NodeResultInfo? CurrentNodeInfo { get; set; }
        
        /// <summary>
        /// Workflow tamamlandı mı?
        /// </summary>
        public bool IsCompleted { get; set; }
        
        /// <summary>
        /// Workflow başarıyla tamamlandı mı? (StopNode'a ulaştıysa true)
        /// </summary>
        public bool IsSuccessfullyCompleted { get; set; }
        
        // Backward compatibility için eski property'ler (deprecated - CurrentNodeInfo kullanılmalı)
        [Obsolete("Use CurrentNodeInfo instead")]
        public AlertNodeInfo? AlertInfo { get; set; }
        
        [Obsolete("Use CurrentNodeInfo instead")]
        public bool FormNodeCompleted { get; set; }
        
        [Obsolete("Use CurrentNodeInfo instead")]
        public string? CompletedFormNodeId { get; set; }
    }
}
