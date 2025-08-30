using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vesa.core.DTOs.DepartmentUserDto
{
    public class DepartmentUserUpdateDto
    {
        public Guid Id { get; set; }
        public Guid TicketDepartmentId { get; set; }
        public string UserId { get; set; }
    }
}
