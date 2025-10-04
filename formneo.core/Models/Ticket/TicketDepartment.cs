using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace formneo.core.Models.Ticket
{
    public class TicketDepartment : BaseEntity
    {
        public string DeparmentCode { get; set; }

        public string DepartmentText { get; set; }

        public bool IsActive { get; set; }

        public string? ManagerId { get; set; }

        public virtual UserApp Manager { get; set; }
        public virtual List<UserApp>? employess { get; set; }

        public virtual List<Tickets>? tickets { get; set; }


        public Guid? WorkCompanyId { get; set; }

        public virtual WorkCompany? WorkCompany { get; set; }
        public virtual List<DepartmentUser> DepartmentUsers { get; set; } = new List<DepartmentUser>();

        public Guid? ParentDepartmentId { get; set; }

        [ForeignKey("ParentDepartmentId")]
        public virtual TicketDepartment? ParentDepartment { get; set; }

        public virtual List<TicketDepartment> SubDepartments { get; set; } = new List<TicketDepartment>();
        public bool IsVisibleInList { get; set; }
    }
}
