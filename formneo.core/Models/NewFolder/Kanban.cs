using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vesa.core.DTOs;

namespace vesa.core.Models.NewFolder
{
    public class Kanban : BaseEntity
    {
        public string Priority { get; set; }
        public string RankId { get; set; }
        public string Status { get; set; }
        public string Summary { get; set; }
        public string Tags { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }

        [ForeignKey("UserApp")]
        public string AssigneeId { get; set; }
        public virtual UserApp Assignee { get; set; }

    }
}
