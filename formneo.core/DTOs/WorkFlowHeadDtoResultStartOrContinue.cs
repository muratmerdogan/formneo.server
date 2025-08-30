using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vesa.core.Models;

namespace vesa.core.DTOs
{
    public class WorkFlowHeadDtoResultStartOrContinue
    {
        public string Id { get; set; }

        public string? WorkFlowInfo { get; set; }
    }
}
