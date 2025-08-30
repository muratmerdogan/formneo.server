using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using vesa.core.Models.Ticket;

namespace vesa.core.Models
{
    public class Positions:BaseEntity
    {
        public string Name { get; set; }
        public string? Description { get; set; } = null;

        [ForeignKey("CustomerRef")]
        public Guid? CustomerRefId { get; set; } = null;
        public virtual WorkCompany? CustomerRef { get; set; } = null;
        public virtual List<UserApp> UserApps { get; set; } = new List<UserApp>();


    }
}
