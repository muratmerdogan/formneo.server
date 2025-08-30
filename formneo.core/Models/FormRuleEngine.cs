using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vesa.core.Models
{
    public class FormRuleEngine : BaseEntity
    {

        [ForeignKey("WorkFlowDefination")]
        public Guid WorkFlowDefinationId { get; set; }

        public virtual WorkFlowDefination WorkFlowDefination { get; set; }

        public Guid NodeId { get; set; }
        public string Rulejson { get; set; }
    }
}
