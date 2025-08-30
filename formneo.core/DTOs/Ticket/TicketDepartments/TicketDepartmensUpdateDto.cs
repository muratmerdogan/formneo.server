using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vesa.core.DTOs.DepartmentUserDto;

namespace vesa.core.DTOs.Ticket
{
    public class TicketDepartmensUpdateDto
    {
        public Guid Id { get; set; }

        public string DeparmentCode { get; set; }

        public string DepartmentText { get; set; }

        public bool IsActive { get; set; }

        public string ManagerId { get; set; }

        public string WorkCompanyId { get; set; }

        public List<DepartmentUserInsertDto> DepartmentUsers { get; set; }
        public Guid? ParentDepartmentId { get; set; }
        public bool IsVisibleInList { get; set; }
    }
}
