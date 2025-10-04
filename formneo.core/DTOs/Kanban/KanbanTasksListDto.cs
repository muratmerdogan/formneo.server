using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace formneo.core.DTOs.Kanban
{
    public class KanbanTasksListDto
    {
        public Guid Id { get; set; }
        public string Priority { get; set; }
        public string RankId { get; set; }
        public string Status { get; set; }
        public string Summary { get; set; }
        public string Tags { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }

        [ForeignKey("UserApp")]
        public string AssigneId { get; set; }
        public virtual UserAppDtoOnlyNameId Assignee { get; set; }

    }
}
