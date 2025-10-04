using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace formneo.core.Models.Ticket
{
    public class DepartmentUser:BaseEntity
    {
        [ForeignKey("TicketDepartment")]
        public Guid TicketDepartmentId { get; set; }
        public virtual TicketDepartment TicketDepartment { get; set; }

        [ForeignKey(nameof(User))]
        public string UserId { get; set; }
        public virtual UserApp User { get; set; }

    }
}
