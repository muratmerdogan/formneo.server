using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace formneo.core.Models.Ticket
{
    public class TicketFile : BaseEntity
    {

        [Required]
        [ForeignKey("TicketComment")]
        public Guid TicketCommentId { get; set; }

        public virtual TicketComment TicketComment { get; set; }
        public string Base64 { get; set; }
        public string FileType { get; set; }
        public string FileName { get; set; }

        public string? FilePath { get; set; }
    }
}
