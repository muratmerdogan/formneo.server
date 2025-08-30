using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vesa.core.Models
{


    public class WorkFlowDefination : BaseEntity
    {

        public string? WorkflowName { get; set; }
        public string Defination { get; set; }

        public Boolean IsActive { get; set; }

        public int Revision { get; set; }

        public virtual List<WorkflowHead>? workflows { get; set; }


        public Guid? FormId { get; set; }

        public virtual Form? Form { get; set; }
    }


}
