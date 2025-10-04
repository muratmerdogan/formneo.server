using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace formneo.core.Models.Ticket
{
    public class Assigne
    {
        [Required]
        [ForeignKey("Ticket")]
        public Guid TicketId { get; set; }

        [Required]
        [ForeignKey("UserApp")]
        public string UserId { get; set; }
        public virtual UserApp User { get; set; }

        [Required]
        [EnumDataType(typeof(AssigneRole))]
        public AssigneRole AssigneRole { get; set; }

    }

    public enum AssigneRole
    {
        [Description("View")]
        View = 1,
        [Description("Edit")]
        Edit = 2,
    }
}
