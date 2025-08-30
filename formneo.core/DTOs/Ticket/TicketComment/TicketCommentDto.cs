using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vesa.core.Models.Ticket;
using vesa.core.DTOs.Ticket.TicketComment;

namespace vesa.core.DTOs.Ticket
{
    public class TicketCommentDto
    {
        public string? id { get; set; }

        public string? TicketId { get; set; }

        public string Body { get; set; }

        public virtual List<TicketFileDto> Files { get; set; }

        public bool isNew { get; set; }

        public DateTime CreatedDate { get; set; }

        public string CreatedBy { get; set; }
        public string? FilePath { get; set; }
    }


    public class TicketCommentInsertDto
    {
        public string Body { get; set; }
        public string? FilePath { get; set; }

        public virtual List<TicketFileInsertDto> Files { get; set; }

        public bool isNew { get; set; }
    }
}
