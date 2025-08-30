using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vesa.core.Models.NewFolder;
using vesa.core.Repositories;

namespace vesa.repository.Repositories
{
    public class KanbanRepository : GenericRepository<Kanban>, IKanbanRepository
    {
        public KanbanRepository(AppDbContext context) : base(context)
        {
        }
    }
}
