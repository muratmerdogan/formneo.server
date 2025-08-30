using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vesa.core.Models;
using vesa.core.Models.Ticket;

namespace vesa.core.DTOs.Ticket.TicketTeams
{
    public class TicketTeamListDto
    {

        public string Id { get; set; }
        public string Name { get; set; }
        public Guid? DepartmentId { get; set; }
        public string ManagerId { get; set; }
        public List<TicketTeamUserAppInsertDto> TeamList { get; set; }

        public TicketDepartment Department { get; set; }
        public UserApp Manager { get; set; }

        public Guid? WorkCompanyId { get; set; }

        public WorkCompany? WorkCompany { get; set; }
    }
}
