using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vesa.core.Models;
using vesa.core.Repositories;

namespace vesa.repository.Repositories
{
    public class ProjectTasksRepository : GenericRepository<ProjectTasks>, IProjectTasksRepository
    {
        public ProjectTasksRepository(AppDbContext context) : base(context)
        {
        }
    }
}
