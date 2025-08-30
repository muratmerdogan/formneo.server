using vesa.core.Models.Ticket;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vesa.core.Models.Ticket;
using vesa.core.Models;
using vesa.core.DTOs.Ticket.TicketAssigne;
using vesa.core.DTOs.Ticket.TicketNotifications;

namespace vesa.core.DTOs.Ticket.Tickets
{
    public class TicketUpdateDto
    {
        public string? Id { get; set; }
        public string? TicketCode { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }

        public string WorkCompanyId { get; set; }

        public string? WorkCompanySystemInfoId { get; set; }

        public string UserAppId { get; set; }

        public TicketType Type { get; set; } // Enum türünde Ticket Type (ör: Bug, Feature)
        public TicketSLA TicketSLA { get; set; } // Enum türünde Ticket Priority (ör: High, Medium)
        public TicketSubject TicketSubject { get; set; } // Enum türünde Ticket Subject(ör: Genel, Medium)

        public TicketStatus Status { get; set; } // Enum türünde Ticket Subject(ör: Genel, Medium)

        public TicketPriority Priority { get; set; } // Enum türünde Ticket Subject(ör: Genel, Medium)

        //public List<TicketCommentDto?> TicketComment { get; set; } // Enum türünde Ticket Subject(ör: Genel, Medium)
        public string? TicketDepartmentId { get; set; }

        public bool isSend { get; set; }
        public string CustomerRefId { get; set; }
        public string? AddedMailAddresses { get; set; }
        public DateTime? EstimatedDeadline { get; set; }
        public Guid? TicketProjectId { get; set; }
    }
    public class TicketUpdateManagerDto
    {
        public string? Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string WorkCompanyId { get; set; }
        public string? WorkCompanySystemInfoId { get; set; }
        public string UserAppId { get; set; }
        public Guid? TicketDepartmentId { get; set; }

        public TicketType Type { get; set; } // Enum türünde Ticket Type (ör: Bug, Feature)
        public TicketSLA TicketSLA { get; set; } // Enum türünde Ticket Priority (ör: High, Medium)
        public TicketSubject TicketSubject { get; set; } // Enum türünde Ticket Subject(ör: Genel, Medium)

        public TicketPriority Priority { get; set; } // Enum türünde Ticket Subject(ör: Genel, Medium)

        public TicketStatus Status { get; set; } // Enum türünde Ticket Subject(ör: Genel, Medium)

        public bool isSend { get; set; }
    }
    public class TicketManagerUpdateDto
    {
        public TicketUpdateDto managerDto { get; set; }
        public TicketAssigneDto assigngDto { get; set; }

        public List<TicketNotificationsInsertDto>? notificationsInsertDtos { get; set; } = null;

    }
}
