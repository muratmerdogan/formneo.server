using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vesa.core.DTOs.Kanban
{
    public class KanbanTasksUpdateDto
    {
        public Guid Id { get; set; }
        public string Priority { get; set; }
        public string RankId { get; set; }
        public string Status { get; set; }
        public string Summary { get; set; }
        public string Tags { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public string AssigneId { get; set; }
    }
}
