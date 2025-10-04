using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using formneo.core.Models.NewFolder;
using formneo.core.Repositories;

namespace formneo.repository.Repositories
{
    public class KanbanRepository : GenericRepository<Kanban>, IKanbanRepository
    {
        public KanbanRepository(AppDbContext context) : base(context)
        {
        }
    }
}
