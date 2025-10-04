using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace formneo.core.Models.Ticket
{
    public class TicketComment : BaseEntity
    {
        [Required]
        [ForeignKey("Ticket")]
        public Guid TicketId { get; set; }
        public virtual Tickets Ticket { get; set; }

        public string Body { get; set; }
        public virtual List<TicketFile> Files { get; set; }

        public string? FilePath { get; set; }
    }
}
