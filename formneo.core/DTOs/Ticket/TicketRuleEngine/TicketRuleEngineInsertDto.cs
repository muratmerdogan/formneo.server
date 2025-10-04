using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace formneo.core.DTOs.Ticket.TicketRuleEngine
{
    public class TicketRuleEngineInsertDto
    {
        public string RuleName { get; set; }
        public int Order { get; set; }
        public string RuleJson { get; set; }
        public Guid AssignedUserId { get; set; }         // Atanacak Takım
        public Guid AssignedTeamId { get; set; }         // Atanacak Takım
        public Guid AssignedDepartmentId { get; set; }   // Atanacak Departman
        public Guid WorkflowId { get; set; }
        public bool IsActive { get; set; }
        public int createEnvironment { get; set; }
    }
}
