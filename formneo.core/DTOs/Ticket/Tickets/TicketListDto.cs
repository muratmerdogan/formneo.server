using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vesa.core.DTOs.Ticket.TicketAssigne;
using vesa.core.DTOs.Ticket.TicketDepartments;
using vesa.core.DTOs.Ticket.TicketNotifications;
using vesa.core.Models.Ticket;

namespace vesa.core.DTOs.Ticket.Tickets
{
    public class TicketDtoResult
    {
        public int Count { get; set; }

        public List<TicketListDto> TicketList { get; set; }
    }

    public class TicketFilters
    {
        public string? workCompanyId { get; set; }
        public string? assignedUser { get; set; }
        public string? assignedTeam { get; set; }
        public string? type { get; set; }
        public string? endDate { get; set; }
        public string? startDate { get; set; }
        public string? creator { get; set; }
        public string? customer { get; set; }
        public string? talepNo { get; set; }
        public string? talepBaslik { get; set; }
        public List<string>? departmentId { get; set; }
        public List<string>? ticketProjectId { get; set; }
    }

    public class TicketListDto
    {
        public Guid Id { get; set; }
        public string TicketCode { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Guid WorkCompanyId { get; set; }
        public string WorkCompanyName { get; set; }
        public Guid? WorkCompanySystemInfoId { get; set; }
        public string? WorkCompanySystemName { get; set; }
        public string UserAppId { get; set; }
        public string UserAppName { get; set; }
        public string UserAppUserName { get; set; }
        public TicketStatus Status { get; set; }
        public string StatusText { get; set; }
        public TicketType Type { get; set; }
        public string TypeText { get; set; }
        public TicketPriority Priority { get; set; }
        public string PriorityText { get; set; }
        public TicketSubject TicketSubject { get; set; }
        public string TicketSubjectText { get; set; }
        public DateTime? ActualStartDate { get; set; }
        public DateTime? ActualEndDate { get; set; }
        public ApproveStatus ApproveStatus { get; set; }
        public string ApproveStatusText { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public TicketSLA TicketSLA { get; set; }
        public string TicketSLAText { get; set; }
        public List<TicketCommentDto> TicketComment { get; set; }
        public string TicketAssigneText { get; set; }
        public string TicketAssigneId { get; set; }
        public bool isTeam { get; set; }
        public TicketAssigneDto TicketAssigne { get; set; }
        public string TicketDepartmentId { get; set; }
        public string TicketDepartmentText{ get; set; }
        public string WorkFlowHeadId { get; set; }
        public int TicketNumber { get; set; }
        public Boolean CanEdit { get; set; }

        public string AssigneDescription { get; set; }

        public Guid? CustomerRefId { get; set; }
        public string? CustomerRefName { get; set; }
        public bool IsFromEmail { get; set; }

        public List<TicketNotificationsListDto> TicketNotificationsListDto { get; set; }

        public string? MailConversationId { get; set; }

        public string? AddedMailAddresses { get; set; }

        public bool? IsFilePath { get; set; } = false;

        public string? FilePath { get; set; } = null;

        public DateTime? EstimatedDeadline { get; set; }
        public Guid? TicketProjectId { get; set; }
        public string? TicketprojectName { get; set; }
    }
}
