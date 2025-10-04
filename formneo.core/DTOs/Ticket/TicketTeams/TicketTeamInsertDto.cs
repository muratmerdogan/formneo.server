using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using formneo.core.Models.Ticket;
using formneo.core.Models;

namespace formneo.core.DTOs.Ticket.TicketTeams
{
    public class TicketTeamInsertDto
    {
        public string Name { get; set; }

        public Guid? DepartmentId { get; set; }

        public string ManagerId { get; set; }

        public  List<TicketTeamUserAppInsertDto> TeamList { get; set; }

        public string WorkCompanyId { get; set; }

    }
}
