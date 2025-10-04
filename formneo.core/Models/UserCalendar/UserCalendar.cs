using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace formneo.core.Models.TaskManagement
{
    public class UserCalendar : BaseEntity
    {

        public string? Name { get; set; }

        public string? Description { get; set; }
        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        [ForeignKey("CustomerRef")]
        public Guid? CustomerRefId { get; set; } = null;
        public virtual WorkCompany? CustomerRef { get; set; } = null;

        [ForeignKey("UserApp")]
        public string UserAppId { get; set; }
        public virtual UserApp UserApp { get; set; }
        public string? Percentage { get; set; } = null;

        public WorkLocation? WorkLocation { get; set; }
        public bool IsAvailable { get; set; }
    }

    public enum WorkLocation
    {
        [Description("Ofis")]
        office = 1,
        [Description("Ev")]
        home = 2,
        [Description("Müşteri")]
        customer = 3,
        [Description("Diğer")]
        other = 4,
    }
}
