using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace vesa.core.Models
{
    public class WorkCompany :BaseEntity
    {
        [Key]
        public Guid Id { get; set; }

        public string Name { get; set; }

        public ApproveWorkDesign ApproveWorkDesign { get; set; }


        public string? UserAppId { get; set; }
        public virtual UserApp? UserApp { get; set; }

        [ForeignKey("WorkFlowDefination")]
        public Guid? WorkFlowDefinationId { get; set; }
        public virtual WorkFlowDefination? WorkFlowDefination { get; set; }
        public bool? IsActive { get; set; }

    }
    public enum ApproveWorkDesign
    {

        [Description("Onay Yok")]
        NoApprove = 0,
        [Description("Onay Var")]
        HasApprove = 1,                // Açık
        //[Description("Assigned")]
        //Assigned = 2,
    }
}
