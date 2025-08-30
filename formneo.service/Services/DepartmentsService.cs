using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vesa.core.DTOs;
using vesa.core.DTOs.Departments;
using vesa.core.Models;
using vesa.core.Repositories;
using vesa.core.Services;
using vesa.core.UnitOfWorks;
using vesa.repository.Repositories;
using vesa.repository.UnitOfWorks;

namespace vesa.service.Services
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
            CompanyName=x.Company.Name,
            CreatedDate=x.CreatedDate,
            DepartmentText=x.DepartmentText,
            Id=x.Id,
            PlantName=x.Plant.Name,
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
            if (dto.CompanyId.HasValue)
            {
                query = query.Where(x => x.CompanyId == dto.CompanyId);
            }
            if (dto.PlantId.HasValue)
            {
                query = query.Where(x => x.PlantId == dto.PlantId);
            }
            query = query.Include("MainClient").Include("Company").Include("Plant");
            var values=await query.ToListAsync();
            //var values= await _departmentsRepository.Where(x=>x.MainClientId==dto.ClientId && x.CompanyId==dto.CompanyId && x.PlantId==dto.PlantId).Include("MainClient").Include("Company").Include("Plant").ToListAsync();
            return values.Select(y => new DepartmentListAllNameDto
            {
                ClientName=y.MainClient.Name,
                Code =y.Code,
                CompanyName =y.Company.Name,
                CreatedDate =y.CreatedDate,
                DepartmentText=y.DepartmentText,
                Id =y.Id,
                PlantName =y.Plant.Name,
                UpdatedDate=y.UpdatedDate,
            }).ToList();
        }
    }
}
