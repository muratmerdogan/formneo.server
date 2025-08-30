using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vesa.core.Models
{
    public class WorkCompanySystemInfo : BaseEntity
    {
    
        public string Name { get; set; }

        [ForeignKey("WorkCompany")]
        public Guid? WorkCompanyId { get; set; }

        public virtual WorkCompany? WorkCompany { get; set; }

    }
}
