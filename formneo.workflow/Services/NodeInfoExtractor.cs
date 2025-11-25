using System;
using System.Collections.Generic;
using System.Linq;
using formneo.core.DTOs;
using formneo.core.Models;
using Newtonsoft.Json.Linq;

namespace formneo.workflow.Services
{
    /// <summary>
    /// Strategy Pattern ile her node tipi için bilgi çıkarma işlemi
    /// </summary>
    public interface INodeInfoExtractor
    {
        string NodeType { get; }
        NodeResultInfo? Extract(WorkflowItem item, JObject? nodeDefinition, JObject? workflowDefinition);
    }

    /// <summary>
    /// AlertNode için bilgi çıkarıcı
    /// </summary>
    public class AlertNodeInfoExtractor : INodeInfoExtractor
    {
        public string NodeType => "alertNode";

        public NodeResultInfo? Extract(WorkflowItem item, JObject? nodeDefinition, JObject? workflowDefinition)
        {
            if (nodeDefinition == null) return null;

            var data = nodeDefinition["data"];
            var alertType = data?["type"]?.ToString()?.ToLower() ?? "error";
            
            // AlertNode sadece error ve warning için kullanılır
            // Success ve info tipindeki alert'ler alert node'da kullanılmaz (normal component'te gösterilir)
            // Eğer success veya info tipi gelirse (olmayacak ama güvenlik için), error olarak işaretle
            if (alertType != "error" && alertType != "warning")
            {
                alertType = "error"; // Fallback: error olarak işaretle
            }
            
            return new AlertNodeInfo
            {
                NodeId = item.NodeId,
                NodeType = NodeType,
                NodeName = item.NodeName,
                Status = item.workFlowNodeStatus,
                Title = data?["title"]?.ToString() ?? "Bildirim",
                Message = data?["message"]?.ToString() ?? "",
                Type = alertType
            };
        }
    }

    /// <summary>
    /// FormNode için bilgi çıkarıcı
    /// </summary>
    public class FormNodeInfoExtractor : INodeInfoExtractor
    {
        public string NodeType => "formNode";

        public NodeResultInfo? Extract(WorkflowItem item, JObject? nodeDefinition, JObject? workflowDefinition)
        {
            if (nodeDefinition == null) return null;

            var data = nodeDefinition["data"];
            var availableActions = new List<string>();
            
            // Workflow definition'dan bu node'un çıkış edge'lerini bul
            if (workflowDefinition != null)
            {
                var edges = workflowDefinition["edges"] as JArray;
                if (edges != null)
                {
                    var outgoingEdges = edges.Where(e => e["source"]?.ToString() == item.NodeId);
                    availableActions = outgoingEdges
                        .Select(e => e["sourceHandle"]?.ToString())
                        .Where(a => !string.IsNullOrEmpty(a))
                        .Distinct()
                        .ToList()!;
                }
            }

            return new FormNodeInfo
            {
                NodeId = item.NodeId,
                NodeType = NodeType,
                NodeName = item.NodeName,
                Status = item.workFlowNodeStatus,
                IsCompleted = item.workFlowNodeStatus == WorkflowStatus.Completed,
                RequiresAction = item.workFlowNodeStatus == WorkflowStatus.Pending,
                AvailableActions = availableActions
            };
        }
    }

    /// <summary>
    /// ApproverNode için bilgi çıkarıcı
    /// </summary>
    public class ApproverNodeInfoExtractor : INodeInfoExtractor
    {
        public string NodeType => "approverNode";

        public NodeResultInfo? Extract(WorkflowItem item, JObject? nodeDefinition, JObject? workflowDefinition)
        {
            if (nodeDefinition == null) return null;

            var data = nodeDefinition["data"];
            var availableActions = new List<string> { "APPROVE", "REJECT" };
            
            // Routes'dan available actions'ları al
            var routes = data?["routes"] as JArray;
            if (routes != null)
            {
                availableActions = routes.Select(r => r.ToString().ToUpper()).ToList();
            }

            return new ApproverNodeInfo
            {
                NodeId = item.NodeId,
                NodeType = NodeType,
                NodeName = item.NodeName,
                Status = item.workFlowNodeStatus,
                ApproverName = data?["approvername"]?.ToString() ?? "",
                ApproverUserName = data?["staticuser"]?.ToString() ?? "",
                IsPending = item.workFlowNodeStatus == WorkflowStatus.Pending,
                AvailableActions = availableActions
            };
        }
    }

    /// <summary>
    /// ScriptNode için bilgi çıkarıcı
    /// </summary>
    public class ScriptNodeInfoExtractor : INodeInfoExtractor
    {
        public string NodeType => "scriptNode";

        public NodeResultInfo? Extract(WorkflowItem item, JObject? nodeDefinition, JObject? workflowDefinition)
        {
            if (nodeDefinition == null) return null;

            // ScriptNode için özel bilgi yoksa genel bilgi döndür
            return new ScriptNodeInfo
            {
                NodeId = item.NodeId,
                NodeType = NodeType,
                NodeName = item.NodeName,
                Status = item.workFlowNodeStatus,
                HasError = false // WorkflowEngine'de hata varsa buraya eklenebilir
            };
        }
    }

    /// <summary>
    /// SqlConditionNode için bilgi çıkarıcı
    /// </summary>
    public class SqlConditionNodeInfoExtractor : INodeInfoExtractor
    {
        public string NodeType => "sqlConditionNode";

        public NodeResultInfo? Extract(WorkflowItem item, JObject? nodeDefinition, JObject? workflowDefinition)
        {
            if (nodeDefinition == null) return null;

            return new SqlConditionNodeInfo
            {
                NodeId = item.NodeId,
                NodeType = NodeType,
                NodeName = item.NodeName,
                Status = item.workFlowNodeStatus,
                ConditionResult = item.workFlowNodeStatus == WorkflowStatus.Completed,
                SelectedPath = null // WorkflowEngine'den alınabilir
            };
        }
    }

    /// <summary>
    /// StopNode için bilgi çıkarıcı
    /// </summary>
    public class StopNodeInfoExtractor : INodeInfoExtractor
    {
        public string NodeType => "stopNode";

        public NodeResultInfo? Extract(WorkflowItem item, JObject? nodeDefinition, JObject? workflowDefinition)
        {
            if (nodeDefinition == null) return null;

            var data = nodeDefinition["data"];
            var stopType = data?["stoptype"]?["code"]?.ToString() ?? "success";

            return new StopNodeInfo
            {
                NodeId = item.NodeId,
                NodeType = NodeType,
                NodeName = item.NodeName,
                Status = item.workFlowNodeStatus,
                StopType = stopType,
                Message = data?["stoptype"]?["name"]?.ToString()
            };
        }
    }

    /// <summary>
    /// NodeInfoExtractor Factory - Strategy Pattern
    /// </summary>
    public class NodeInfoExtractorFactory
    {
        private readonly Dictionary<string, INodeInfoExtractor> _extractors;

        public NodeInfoExtractorFactory()
        {
            _extractors = new Dictionary<string, INodeInfoExtractor>
            {
                { "alertNode", new AlertNodeInfoExtractor() },
                { "formNode", new FormNodeInfoExtractor() },
                { "approverNode", new ApproverNodeInfoExtractor() },
                { "scriptNode", new ScriptNodeInfoExtractor() },
                { "sqlConditionNode", new SqlConditionNodeInfoExtractor() },
                { "stopNode", new StopNodeInfoExtractor() }
            };
        }

        public INodeInfoExtractor? GetExtractor(string nodeType)
        {
            return _extractors.TryGetValue(nodeType, out var extractor) ? extractor : null;
        }

        public NodeResultInfo? ExtractNodeInfo(WorkflowItem item, JObject? workflowDefinition)
        {
            if (workflowDefinition == null || string.IsNullOrEmpty(item.NodeId)) 
                return null;

            var nodes = workflowDefinition["nodes"] as JArray;
            if (nodes == null) 
                return null;

            var nodeDefinition = nodes.FirstOrDefault(n => n["id"]?.ToString() == item.NodeId) as JObject;
            if (nodeDefinition == null) 
                return null;

            var extractor = GetExtractor(item.NodeType);
            return extractor?.Extract(item, nodeDefinition, workflowDefinition);
        }
    }
}

