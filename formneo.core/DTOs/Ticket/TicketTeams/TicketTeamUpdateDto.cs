using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace formneo.core.DTOs.Ticket.TicketTeams
{
    public class TicketTeamUpdateDto
    {

        public string Id { get; set; }
        public string Name { get; set; }

        public Guid? DepartmentId { get; set; }

        public string ManagerId { get; set; }

        public List<TicketTeamUserAppUpdateDto> TeamList { get; set; }

        public Guid? WorkCompanyId { get; set; }
    }
}
