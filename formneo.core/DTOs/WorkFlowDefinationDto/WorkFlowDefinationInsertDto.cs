using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vesa.core.Models;

namespace vesa.core.DTOs
{
    public class WorkFlowDefinationInsertDto
    {
        public string? WorkflowName { get; set; }
        public string Defination { get; set; }

        public Boolean IsActive { get; set; }

        public int Revision { get; set; }


    }
}
