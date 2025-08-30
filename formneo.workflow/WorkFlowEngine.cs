
using Microsoft.AspNetCore.Mvc.Routing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net.Mail;
using System.Net;
using System.Reflection.Metadata;
using vesa.core.Models;
using vesa.core.Services;
using vesa.workflow;
using Microsoft.AspNetCore.Mvc;
using vesa.core.Operations;
using System.Security.Cryptography;
using Newtonsoft.Json;
using System.Dynamic;
using System.Linq.Dynamic.Core;
using Newtonsoft.Json.Linq;
using JsonLogic.Net;
using static System.Runtime.InteropServices.JavaScript.JSType;

public class WorkflowNode
{
    public string Id { get; set; }
    public string Type { get; set; }
    public Position Position { get; set; }
    public string ClassName { get; set; }
    public NodeData Data { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }
    public bool Selected { get; set; }
    public Position PositionAbsolute { get; set; }
    public bool Dragging { get; set; }

    public Guid HeadId { get; set; }


    public WorkflowItem Execute()
    {

        WorkflowItem item = new WorkflowItem();
        item.approveItems = new List<ApproveItems>();

        item.WorkflowHeadId = HeadId;
        item.NodeName = Data.Name != null ? Data.Name : "WorkFlowName";
        item.NodeType = Type;
        item.NodeDescription = Data.Name != null ? Data.Name : "Description";
        item.CreatedDate = DateTime.Now;
        item.NodeId = Id;
        return item;
    }
}
public class Position
{
    public float X { get; set; }
    public float Y { get; set; }
}

public class NodeData
{
    public string Name { get; set; }
    public string Text { get; set; }
    public string approvername { get; set; }
    public bool isManager { get; set; }
    public string code { get; set; }
    public string sqlQuery { get; set; }
    public stoptype stoptype { get; set; }
}

public class stoptype
{
    public string name { get; set; }
    public string code { get; set; }
}

public class Edges
{
    public string Source { get; set; }
    public string SourceHandle { get; set; }
    public string Target { get; set; }
    public string TargetHandle { get; set; }
    public bool Animated { get; set; }
    public LinkStyle Style { get; set; }
    public string Id { get; set; }
    public bool Selected { get; set; }
}



public class LinkStyle
{
    public string Stroke { get; set; }
}

// Kullanım örneği
public class WorkflowExample
{
    public List<WorkflowNode> Nodes { get; set; }
    public List<Edges> Edges { get; set; }
    public Viewport Viewport { get; set; }
    public int FirstNode { get; set; }
}

public class Viewport
{
    public float X { get; set; }
    public float Y { get; set; }
    public float Zoom { get; set; }
}


public class Workflow
{

    public Guid _HeadId { get; set; }

    public List<WorkflowNode> Nodes { get; set; }
    public List<Edges> Edges { get; set; }
    public Dictionary<string, object> Properties { get; set; }

    public List<WorkflowItem>  _workFlowItems;

    private WorkflowHead _workFlowHead;

    public WorkFlowParameters _parameters;

    public string _payloadJson;

    public string _ApiSendUser { get; set; }

    public void Start(string apiSendUser,string payloadJson)
    {
        _ApiSendUser = apiSendUser;
        _payloadJson = payloadJson;
        // İş akışını başlatmak için ilk düğümü bulun

        WorkflowNode startNode;

        startNode = Nodes.Find(node => node.Type == "startNode");


        ExecuteNode(startNode.Id, "");
    }

    public void Continue(WorkflowItem workfLowItem, string nodeId, string apiSendUser, string Parameter = "", WorkflowHead head = null, WorkFlowDefination defination = null,string payloadJson=null)
    {


        _ApiSendUser = apiSendUser;
        // İş akışını başlatmak için ilk düğümü bulun
        WorkflowNode startNode;
        _workFlowHead = head;

        startNode = Nodes.Find(node => node.Id == nodeId);


        // İlk düğümü çalıştırın
        ExecuteNode(startNode.Id, Parameter, workfLowItem);

    }

    private void ExecuteNode(string nodeId, string Parameter = "", WorkflowItem workflowItem =null)
    {

        _ApiSendUser = _ApiSendUser;

        WorkflowNode currentNode = Nodes.Find(node => node.Id == nodeId);
        currentNode.HeadId = _HeadId;


        WorkflowItem result = null;
        // Düğümü çalıştırın
        if (workflowItem != null)
        {
            result = workflowItem;
        }
        else
        {
            result = currentNode.Execute();
        }

        if (result.NodeType == "stopNode")
        {
            string nextNode = ExecuteStopNode(currentNode, result, Parameter);

            if (nextNode != "" && nextNode != null)
            {
                ExecuteNode(nextNode);

            }
            else
            {
                return;
            }

        }

        if (result.NodeType == "sqlConditionNode")
        {
            string nextNode = ExecuteSqlConditionNode(currentNode, result, Parameter);

            if (nextNode != "" && nextNode != null)
            {
                ExecuteNode(nextNode);

            }
            else
            {
                return;
            }

        }
        
        if (result.NodeType == "approverNode")
        {
            string nextNode = ExecuteApprove(currentNode, result, Parameter);

            if (nextNode != "" && nextNode != null)
            {
                ExecuteNode(nextNode);

            }
            else
            {
                return;
            }

        }
        if (result.NodeType == "EmailNode")
        {
            //string nextNode = ExecuteMailNode(currentNode, result, Parameter);

            //if (nextNode != "")
            //{
            //    ExecuteNode(nextNode);

            //}
            //else
            //{
            //    return;
            //}
        }
        if (result.NodeType != "EmailNode" && result.NodeType != "ApproverNode")
        {

            if (_workFlowItems.Contains(result))
            {

                return;
            }


            // Düğüme bağlı çıkış bağlantılarını bulun
            List<Edges> outgoingLinks = Edges.FindAll(link => link.Source == nodeId);
            result.workFlowNodeStatus = WorkflowStatus.Completed;



            _workFlowItems.Add(result);
            // Her çıkış bağlantısını takip edin
            foreach (var link in outgoingLinks)
            {
                string nextNodeId = link.Target;

                // Bir sonraki düğümü çalıştırın
                ExecuteNode(nextNodeId);
            }
        }
    }

    private string ExecuteSqlConditionNode(WorkflowNode currentNode, WorkflowItem workFlowItem, string parameter)
    {
        //too

        //var normalized = JObject.Parse(JsonConvert.SerializeObject(JObject.Parse(_payloadJson)));
        //var list = new List<JObject> { normalized };

        //var expression = "TicketSubject == 2";
        //var result = list.AsQueryable().Where(expression).ToList();

        var rule = JObject.Parse(currentNode.Data.sqlQuery);

        var data = JObject.Parse(_payloadJson);
        // Create an evaluator with default operators.
        var evaluator = new JsonLogicEvaluator(EvaluateOperators.Default);

        // Apply the rule to the data.
        object result = evaluator.Apply(rule, data);


        //var expression = "Type  == 1";
        //var result = System.Linq.Dynamic.Core.DynamicExpressionParser.ParseLambda<ExpandoObject, bool>(new ParsingConfig(), false, expression).Compile().Invoke(payload);

        ////currentNode.Data.sqlQuery

        //var result = Utils.ExecuteSql(currentNode.Data.sqlQuery, _HeadId.ToString());


        if ((bool)result)
        {
            parameter = "yes";
        }

        // Düğüme bağlı çıkış bağlantılarını bulun
        var nextNode = FindLinkForPort(currentNode.Id, parameter);
        return nextNode;
    }

    private  string ExecuteApprove(WorkflowNode currentNode,WorkflowItem workFlowItem ,string parameter)
    {
        //too

        if (parameter == "")
        {
            _workFlowItems.Add(workFlowItem);

            Utils utils = new Utils();
            string approverUserNameSurname = utils.GetNameAndSurnameAsync(currentNode.Data!.code).ToString();

            workFlowItem.workFlowNodeStatus = WorkflowStatus.Pending;
            if (currentNode.Data.isManager == true)
            {
                var x = _ApiSendUser;

                PositionCreateRunner runner = new PositionCreateRunner();
                string managerId = runner.GetManagerId(_ApiSendUser).Result.ToString();
                var managerDisplayName =  runner.GetManagerDisplayName(managerId).Result.ToString();
                workFlowItem.approveItems.Add(new ApproveItems { ApproveUser = managerId, WorkFlowDescription = "", ApproveUserNameSurname = managerDisplayName });
                return "";
            }
            else
            {
                workFlowItem.approveItems.Add(new ApproveItems { ApproveUser = currentNode.Data!.code, WorkFlowDescription = "", ApproveUserNameSurname = approverUserNameSurname });
                return "";
            }
            return "";
        }
        
        // Düğüme bağlı çıkış bağlantılarını bulun

        if (parameter == "yes" || parameter == "no")
        {
            workFlowItem.workFlowNodeStatus = WorkflowStatus.Completed;
        }
        var nextNode=  FindLinkForPort(currentNode.Id, parameter);
        return nextNode;
    }
    private string ExecuteStopNode(WorkflowNode currentNode, WorkflowItem workFlowItem, string parameter)
    {
        //too
        if(currentNode.Data.stoptype.code=="FINISH")
        {
            /// _HeadId
            /// 
            _workFlowHead.workFlowStatus = WorkflowStatus.Completed;

            var result = _parameters._ticketService.Where(e => e.WorkflowHeadId == _HeadId).FirstOrDefault();

            result!.Status = vesa.core.Models.Ticket.TicketStatus.Open;

            _parameters._ticketService.UpdateTicket(result);

            //if (_workFlowHead.WorkFlowDefinationId == new Guid("521d0cf8-c5a4-42ff-a031-aa61ab319e4a"))
            //{
            //    var positionRunner = new PositionCreateRunner();
            //    positionRunner.RunAsync(_HeadId.ToString());
            //}
            //else if (_workFlowHead.WorkFlowDefinationId == new Guid("b08b506c-1d98-4feb-a680-64ddc67b3c39"))
            //{
            //    var normRunner = new NormCreateRunner();
            //    normRunner.RunAsync(_HeadId.ToString());
            //}
        }
        else
        {

            var result = _parameters._ticketService.Where(e => e.WorkflowHeadId == _HeadId).FirstOrDefault();

            result!.Status = vesa.core.Models.Ticket.TicketStatus.Draft;

            _parameters._ticketService.UpdateTicket(result);
        }

        return "";
    }

    //private string ExecuteMailNode(WorkflowNode currentNode, WorkflowItem workFlowItem, string parameter)
    //{
    //    //too

    //    //List<WorkflowLink> outgoingLinks = Links.FindAll(link => link.FromNode == currentNode.Id);


    //    //EmailNode emailNode = new EmailNode(currentNode.Configuration, workFlowItem);




    //    //workFlowItem.workFlowNodeStatus = WorkflowStatus.Completed;

    //    //_workFlowItems.Add(workFlowItem);

    //    //string nextNodeId = "";
    //    //// Her çıkış bağlantısını takip edin
    //    //foreach (var link in outgoingLinks)
    //    //{
    //    //    nextNodeId = link.ToNode;

    //    //}

    //    //return nextNodeId;
    //}

    private string FindLinkForPort(string fromNode, string port)
    {
        Edges matchingLink = Edges.Find(link => link.Source == fromNode && link.SourceHandle == port && Nodes.Any(node => node.Id == link.Target));

        if (matchingLink != null)
        {
            return matchingLink.Target;
        }

        return null; // If no link with the specified port is found
    }




}
