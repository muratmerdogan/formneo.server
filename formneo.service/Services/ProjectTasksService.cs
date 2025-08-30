using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vesa.core.Models;
using vesa.core.Repositories;
using vesa.core.Services;
using vesa.core.UnitOfWorks;
using vesa.repository.Repositories;

namespace vesa.service.Services
{
    public class ProjectTasksService : Service<ProjectTasks>, IProjectTasksService
    {
        private readonly IProjectTasksRepository _projectTasksRepository;
        public ProjectTasksService(IGenericRepository<ProjectTasks> repository, IUnitOfWork unitOfWork, IProjectTasksRepository projectTasksRepository) : base(repository, unitOfWork)
        {
            _projectTasksRepository = projectTasksRepository;
        }
    }
}
