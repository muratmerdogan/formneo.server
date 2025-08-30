using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace vesa.core.Models.Ticket
{
    public class TicketApprove  : BaseEntity
    {

        [Required]
        [ForeignKey("Tickets")]
        public Guid TicketsId { get; set; }
        public virtual Tickets Tickets { get; set; }

        [ForeignKey("UserApp")]
        public string? UserAppId { get; set; }
        public virtual UserApp? UserApp { get; set; }

  
        //[Required]
        //[EnumDataType(typeof(AssigneRole))]
        //public AssigneRole AssigneRole { get; set; }

    }
}
