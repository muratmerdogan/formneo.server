using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vesa.core.Models.Ticket;
using vesa.core.Models;

namespace vesa.core.DTOs.Ticket.Tickets
{
    public class TicketInsertDto
    {
        public string? TicketCode { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }

        public string WorkCompanyId { get; set; }

        public string? WorkCompanySystemInfoId { get; set; }

        public string UserAppId { get; set; }

        public TicketType Type { get; set; } // Enum türünde Ticket Type (ör: Bug, Feature)
        public TicketSLA TicketSLA { get; set; } // Enum türünde Ticket Priority (ör: High, Medium)
        public TicketSubject TicketSubject { get; set; } // Enum türünde Ticket Subject(ör: Genel, Medium)

        public TicketPriority Priority { get; set; } // Enum türünde Ticket Subject(ör: Genel, Medium)

        public List<TicketCommentInsertDto?> TicketComment { get; set; } // Enum türünde Ticket Subject(ör: Genel, Medium)

        public bool isSend { get; set; }

        //public string? TicketAssigneUserId { get; set; }

        //public string? TicketAssigneTeamId { get; set; }

        public string CustomerRefId { get; set; }
        public bool IsFromEmail { get; set; }
        public string? MailConversationId { get; set; }
        public string? AddedMailAddresses { get; set; }

        public bool? IsFilePath { get; set; } = false;

        public string? FilePath { get; set; } = null;
    }
}
