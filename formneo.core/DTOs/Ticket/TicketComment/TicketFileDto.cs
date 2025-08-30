using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vesa.core.DTOs.Ticket.TicketComment
{
    public class TicketFileDto
    {
        public string? id { get; set; }
        public string? TicketCommentId { get; set; }
        public string? Base64 { get; set; }
        public string? FileType { get; set; }
        public string? FileName { get; set; }
        public string? FilePath { get; set; }

    }

    public class TicketFileInsertDto
    {
        public string? Base64 { get; set; }
        public string? FileType { get; set; }
        public string? FileName { get; set; }
        public string? FilePath { get; set; }
    }
}
