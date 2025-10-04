using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace formneo.core.Models.Ticket
{
    public class TicketAssigneDepartments : BaseEntity
    {
        [Required]
        [ForeignKey("Ticket")]
        public Guid TicketId { get; set; }

        [Required]
        [ForeignKey("Departments")]
        public Guid DepartmentsId { get; set; }
        public virtual Departments Departments { get; set; }

        [Required]
        [EnumDataType(typeof(AssigneRole))]
        public AssigneRole AssigneRole { get; set; }

    }


}
