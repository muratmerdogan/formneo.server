using System;
using System.Collections.Generic;

namespace formneo.core.DTOs
{
    /// <summary>
    /// Base class for all node result information
    /// </summary>
    public abstract class NodeResultInfo
    {
        public string NodeId { get; set; }
        public string NodeType { get; set; }
        public string NodeName { get; set; }
        public formneo.core.Models.WorkflowStatus Status { get; set; }
    }

    /// <summary>
    /// AlertNode için özel bilgiler
    /// NOT: Success ve info mesajları alert node'da kullanılmaz, normal component'te gösterilir
    /// AlertNode sadece error ve warning için kullanılır ve rollback yapar
    /// </summary>
    public class AlertNodeInfo : NodeResultInfo
    {
        public string Title { get; set; }
        public string Message { get; set; }
        public string Type { get; set; } // "error" veya "warning" (success ve info yok - normal component'te gösterilir)
    }

    /// <summary>
    /// FormNode için özel bilgiler
    /// </summary>
    public class FormNodeInfo : NodeResultInfo
    {
        public bool IsCompleted { get; set; }
        public bool RequiresAction { get; set; }
        public List<string> AvailableActions { get; set; } // ["SAVE", "APPROVE", "SUBMIT"]
    }

    /// <summary>
    /// ApproverNode için özel bilgiler
    /// </summary>
    public class ApproverNodeInfo : NodeResultInfo
    {
        public string ApproverName { get; set; }
        public string ApproverUserName { get; set; }
        public bool IsPending { get; set; }
        public List<string> AvailableActions { get; set; } // ["APPROVE", "REJECT", "SENDBACK"]
    }

    /// <summary>
    /// ScriptNode için özel bilgiler
    /// </summary>
    public class ScriptNodeInfo : NodeResultInfo
    {
        public object? ScriptResult { get; set; }
        public bool HasError { get; set; }
        public string? ErrorMessage { get; set; }
    }

    /// <summary>
    /// SqlConditionNode için özel bilgiler
    /// </summary>
    public class SqlConditionNodeInfo : NodeResultInfo
    {
        public bool ConditionResult { get; set; }
        public string? SelectedPath { get; set; } // "yes" or "no"
    }

    /// <summary>
    /// StopNode için özel bilgiler
    /// </summary>
    public class StopNodeInfo : NodeResultInfo
    {
        public string StopType { get; set; } // "success", "error", "cancelled"
        public string? Message { get; set; }
    }
}

