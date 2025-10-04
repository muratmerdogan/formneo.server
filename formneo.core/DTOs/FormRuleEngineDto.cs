using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using formneo.core.Models;

namespace formneo.core.DTOs
{
    public class FormRuleEngineDto
    {
        public Guid Id { get; set; }


        public Guid WorkFlowDefinationId { get; set; }

        public Guid NodeId { get; set; }
        public string Rulejson { get; set; }


    }
}
