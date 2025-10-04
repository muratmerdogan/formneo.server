using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using formneo.core.Models;

namespace formneo.core.DTOs
{
    public class WorkCompanyDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public ApproveWorkDesign ApproveWorkDesign { get; set; }

        public string? UserAppId { get; set; }
        public UserAppDto? UserApp { get; set; }

        public WorkFlowDefination? WorkFlowDefination { get; set; }
        public string? WorkFlowDefinationId { get; set; }

        public bool? IsActive { get; set; }
    }
}
