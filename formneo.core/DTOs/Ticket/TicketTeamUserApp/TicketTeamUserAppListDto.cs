using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vesa.core.Models.Ticket;
using vesa.core.Models;

namespace vesa.core.DTOs.Ticket.TicketTeams
{
    public class TicketTeamUserAppListDto
    {
  
        public Guid TicketTeamId { get; set; }

        public string UserAppId { get; set; }
    }
}
