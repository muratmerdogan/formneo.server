using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using formneo.core.Models;

namespace formneo.core.DTOs.Ticket.TicketNotifications
{
    public class TicketNotificationsInsertDto
    {
        public Guid TicketId { get; set; }
        public string? UserAppId { get; set; }

    }

    public class TicketNotificationsListDto
    {
        public Guid TicketId { get; set; }
        public string? UserAppId { get; set; }

        public UserApp? User { get; set; }
    }
}
