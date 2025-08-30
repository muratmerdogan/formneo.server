using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vesa.core.Models;
using vesa.core.DTOs.DepartmentUserDto;
using vesa.core.Models.Ticket;

namespace vesa.core.DTOs.Ticket.TicketDepartments
{
    public class TicketDepartmensListDto
    {
        public string Id { get; set; }
        public string DeparmentCode { get; set; }

        public string DepartmentText { get; set; }

        public bool IsActive { get; set; }

        public string ManagerId { get; set; }

        //public string ManagerText { get; set; }

        public UserApp Manager { get; set; }

        public Guid? WorkCompanyId { get; set; }

        public virtual WorkCompany? WorkCompany { get; set; }
        public List<DepartmentUserListDto> DepartmentUsers { get; set; }
        public Guid? ParentDepartmentId { get; set; }
        public virtual List<TicketDepartment> SubDepartments { get; set; } = new List<TicketDepartment>();
        public bool IsVisibleInList { get; set; }

    }
    public class MyTicketDepartmentsUserDto
    {
        public string UserId { get; set; }
        public string FullName { get; set; }
    }
}
