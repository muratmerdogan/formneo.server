using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vesa.core.Models.Ticket;
using vesa.core.Models;

namespace vesa.core.DTOs.Ticket.TicketAssigne
{
    public class TicketAssigneDto
    {
        public Guid Id { get; set; }
        public Guid TicketsId { get; set; }

        public Guid? UserAppId { get; set; }

        public Guid? TicketTeamID { get; set; }

        public bool isActive { get; set; }

        public TicketStatus Status { get; set; }
        public string Description { get; set; }

    }
    public class TicketAssigneListDto
    {
        public Guid TicketsId { get; set; }

        public string Name { get; set; }
        public TicketStatus StatusId { get; set; }

        public string  Status { get; set; }

        public DateTime CreateDate { get; set; }
        public string Description { get; set; }
        public string CreatedBy { get; set; }
    }
}
