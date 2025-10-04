using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using formneo.core.DTOs;
using formneo.core.DTOs.Departments;
using formneo.core.Models;
using formneo.core.Repositories;
using formneo.core.Services;
using formneo.core.UnitOfWorks;
using formneo.repository.Repositories;
using formneo.repository.UnitOfWorks;

namespace formneo.service.Services
{
    public class DepartmentsService : Service<Departments>, IDepartmentService
    {
        private readonly IDepartmentsRepository _departmentsRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DepartmentsService(IGenericRepository<Departments> repository, IUnitOfWork unitOfWork, IMapper mapper, IDepartmentsRepository departmentsRepository) : base(repository, unitOfWork)
        {
            _mapper = mapper;
            _departmentsRepository = departmentsRepository;

            _unitOfWork = unitOfWork;

        }

        public async Task<List<DepartmentListAllNameDto>> GetAllClientCompanyPlantNames()
        {
            var values = await _departmentsRepository.GetAll().Include("MainClient").Include("Company").Include("Plant").ToListAsync();
            return values.Select(x => new DepartmentListAllNameDto 
            {
            ClientName=x.MainClient.Name,
            Code=x.Code,

            CreatedDate=x.CreatedDate,
            DepartmentText=x.DepartmentText,
            Id=x.Id,

            UpdatedDate=x.UpdatedDate,
            }).ToList();
        }

        public async Task<IEnumerable<DepartmentsListDto>> GetDepermantListAsync()
        {
            var values = await _departmentsRepository.GetAll().ToListAsync();
            var mapvalues=_mapper.Map<IEnumerable<DepartmentsListDto>>(values);
            return mapvalues;
        }

        public async Task<List<DepartmentListAllNameDto>> GetFilterClientCompanyPlantName(DepartmanFilterDto dto)
        {
            var query = _departmentsRepository.GetAll();
            if (dto.ClientId.HasValue)
            {
                query= query.Where(x => x.MainClientId == dto.ClientId);
            }
         
            query = query.Include("MainClient").Include("Company").Include("Plant");
            var values=await query.ToListAsync();
            //var values= await _departmentsRepository.Where(x=>x.MainClientId==dto.ClientId && x.CompanyId==dto.CompanyId && x.PlantId==dto.PlantId).Include("MainClient").Include("Company").Include("Plant").ToListAsync();
            return values.Select(y => new DepartmentListAllNameDto
            {
                ClientName=y.MainClient.Name,
                Code =y.Code,
                CreatedDate =y.CreatedDate,
                DepartmentText=y.DepartmentText,
                Id =y.Id,
                UpdatedDate=y.UpdatedDate,
            }).ToList();
        }
    }
}
