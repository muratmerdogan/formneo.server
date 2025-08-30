using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vesa.core.Models;

namespace vesa.core.DTOs
{
    public class WorkCompanyInsertDto
    {

        public string Name { get; set; }

        public ApproveWorkDesign ApproveWorkDesign { get; set; }

        public string? UserAppId { get; set; }

        public string? WorkFlowDefinationId { get; set; }

        public bool? IsActive { get; set; }


    }
}
