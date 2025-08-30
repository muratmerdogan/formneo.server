using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace vesa.core.Models.Ticket
{
    public class TicketAssigne  : BaseEntity
    {

        [Required]
        [ForeignKey("Tickets")]
        public Guid TicketsId { get; set; }
        public virtual Tickets Tickets { get; set; }

        [ForeignKey("UserApp")]
        public string? UserAppId { get; set; }
        public virtual UserApp? UserApp { get; set; }

        [ForeignKey("TicketTeam")]
        public Guid? TicketTeamID { get; set; }
        public virtual TicketTeam? TicketTeam { get; set; }

        public bool isActive { get; set; }

        public TicketStatus Status { get; set; }

        public string Description { get; set; }

        //[Required]
        //[EnumDataType(typeof(AssigneRole))]
        //public AssigneRole AssigneRole { get; set; }


    }
}
