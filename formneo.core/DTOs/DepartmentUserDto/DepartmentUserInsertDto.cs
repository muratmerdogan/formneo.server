using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vesa.core.Models.Ticket;
using vesa.core.Models;

namespace vesa.core.DTOs.DepartmentUserDto
{
    public class DepartmentUserInsertDto
    {
        public string? TicketDepartmentId { get; set; } = null;
        public string UserId { get; set; }


    }

    public class DepartmentUserListDto
    {

        public Guid? id { get; set; } = null;
        public Guid? TicketDepartmentId { get; set; } = null;
        public string UserId { get; set; }

        public  UserAppDto User { get; set; }


    }
}
