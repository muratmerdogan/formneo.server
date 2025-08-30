using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vesa.core.Models;
using vesa.core.Repositories;
using vesa.core.Services;
using vesa.core.UnitOfWorks;

namespace vesa.service.Services
{
    public class ProjectCategoriesService : Service<ProjectCategories>, IProjectCategoriesService
    {
        public ProjectCategoriesService(IGenericRepository<ProjectCategories> repository, IUnitOfWork unitOfWork, IMapper mapper, IProjectCategoriesRepository projectCategoriesRepository) : base(repository, unitOfWork)
        {
           
        }
    }
}
