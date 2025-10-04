using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace formneo.core.Models.Ticket
{
    public class TicketTeam : BaseEntity
    {

        public string Name { get; set; }

        // TicketDepartment ile ilişki
        [ForeignKey("Department")]
        public Guid? DepartmentId { get; set; }
        public virtual TicketDepartment? Department { get; set; }

        // Takım yöneticisi
        [ForeignKey("Manager")]
        public string ManagerId { get; set; }
        public virtual UserApp Manager { get; set; }

 
        public virtual List<TicketTeamUserApp> TeamList { get; set; }

        //şirket bilgisi
        [ForeignKey("WorkCompany")]
        public Guid? WorkCompanyId { get; set; }

        public virtual WorkCompany? WorkCompany { get; set; }
    }
}
