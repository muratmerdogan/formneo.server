using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using formneo.core.Models;
using formneo.core.Repositories;
using formneo.core.Services;
using formneo.core.UnitOfWorks;
using formneo.repository.Repositories;

namespace formneo.service.Services
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
