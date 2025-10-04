using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using formneo.core.Models;
using formneo.core.Repositories;
using formneo.core.Services;
using formneo.core.UnitOfWorks;

namespace formneo.service.Services
{
    public class ProjectCategoriesService : Service<ProjectCategories>, IProjectCategoriesService
    {
        public ProjectCategoriesService(IGenericRepository<ProjectCategories> repository, IUnitOfWork unitOfWork, IMapper mapper, IProjectCategoriesRepository projectCategoriesRepository) : base(repository, unitOfWork)
        {
           
        }
    }
}
