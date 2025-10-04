using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace formneo.core.Models.Ticket
{
    public class TicketNotifications : BaseEntity
    {
        [Required]
        [ForeignKey("Ticket")]
        public Guid TicketId { get; set; }
        public virtual Tickets Ticket { get; set; }

        [ForeignKey("UserApp")]
        public string? UserAppId { get; set; }
        public virtual UserApp? UserApp { get; set; }

    }
}
