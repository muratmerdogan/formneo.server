using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using formneo.core.DTOs.ProjectDtos;
using formneo.core.Models;
using formneo.core.Repositories;
using formneo.core.Services;
using formneo.core.UnitOfWorks;

namespace formneo.service.Services
{
    public class ProjectService: Service<Project>, IProjectService
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProjectService(IGenericRepository<Project> repository, IUnitOfWork unitOfWork, IMapper mapper, IProjectRepository projectRepository) : base(repository, unitOfWork)
        {
            _mapper = mapper;
            _projectRepository = projectRepository;
            _unitOfWork = unitOfWork;

        }

        public async Task<IEnumerable<GetProjectListDto>> GetAllProductListAsync()
        {
            var values = await _projectRepository.GetAll().ToListAsync();
            return values != null ? values.Select(y => new GetProjectListDto
            {
                Id=y.Id,
                Description = y.Description,
                Name = y.Name,
                UserId = y.UserId,
                CategoryName= GetEnumDescription((Category)y.CategoryId)
            }).ToList() : new List<GetProjectListDto>();
        }

        public async Task<IEnumerable<GetProjectListDto>> GetByUserProductListAsync(string userId)
        {
            var values= await _projectRepository.Where(x=>x.UserId == userId).ToListAsync();
            return values!=null ? values.Select(y=>new GetProjectListDto
            {
                UserId=y.UserId,
                CategoryName= GetEnumDescription((Category)y.CategoryId),
                Description=y.Description,
                Name = y.Name,
                Id=y.Id
            }).ToList() : new List<GetProjectListDto>();
        }
        private string GetEnumDescription(Category category)
        {
            var field = category.GetType().GetField(category.ToString());
            var attribute = (DescriptionAttribute)Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute));
            return attribute?.Description ?? category.ToString(); // Description yoksa enum adını döndür
        }
    }
}
