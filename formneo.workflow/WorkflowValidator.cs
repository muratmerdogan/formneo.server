using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vesa.workflow
{
    using Newtonsoft.Json.Linq;
    using System;
    using System.Linq;
    using System.Xml.Linq;

    public  class WorkflowValidator
    {
        public bool ValidateWorkflow(string jsonData, out string error)
        {

            JObject workflow = JObject.Parse(jsonData);
            var nodes = workflow["nodes"].ToObject<List<Node>>();
            var edges = workflow["edges"].ToObject<List<Edge>>();

            foreach (var node in nodes)
            {
                if (node.Type == "approverNode")
                {
                    bool yesConnected = edges.Any(e => e.Source == node.Id && e.SourceHandle == "yes");
                    bool noConnected = edges.Any(e => e.Source == node.Id && e.SourceHandle == "no");

                    if (!yesConnected || !noConnected)
                    {
                        error = "Evet ya hayır mutlaka bir akışa bağlı olmalıdır";
                        return false; // Her iki bağlantı da mevcut değilse geçersiz
                    }
                }
            }

            error = "";
            return true; // Tüm kontrol noktaları geçerli
        }

        private bool ValidateNode(JToken node, JArray links, out string error)
        {
            string nodeClazz = (string)node["nodeClazz"];

            if ((string)node["nodeClazz"] == "ApproverNode")
            {


                // Check if there is only one incoming link
                int incomingLinkCount = links.Count(link =>
                    (string)link["toNode"] == (string)node["id"]);

                if (incomingLinkCount != 1)
                {
                    error = "ApproverNode should have exactly one incoming link.";
                    return false;
                }


                // Check if there is an outgoing link for both "Onay" and "Red"
                bool hasOnayLink = links.Any(link =>
                    (string)link["fromNode"] == (string)node["id"] &&
                    (string)link["fromPort"] == "Onay");

                bool hasRedLink = links.Any(link =>
                    (string)link["fromNode"] == (string)node["id"] &&
                    (string)link["fromPort"] == "Red");

                if (!hasOnayLink || !hasRedLink)
                {
                    error = "ApproverNode should be connected to both 'Onay' and 'Red'.";
                    return false;
                }
            }

            // Your remaining validation logic...

            error = "";
            return true;


        }
    }
    public class Node
    {
        public string Id { get; set; }
        public string Type { get; set; }
    }

    public class Edge
    {
        public string Source { get; set; }
        public string Target { get; set; }
        public string SourceHandle { get; set; }
    }
}
