using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using formneo.core.DTOs.Company;
using formneo.core.DTOs.Plants;
using formneo.core.Models;
using formneo.core.Repositories;
using formneo.core.Services;
using formneo.core.UnitOfWorks;

namespace formneo.service.Services
{
    public class PlantService:Service<Plant>,IPlantService
    {
        private readonly IPlantRepository _plantRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PlantService(IGenericRepository<Plant> repository, IUnitOfWork unitOfWork, IMapper mapper, IPlantRepository plantRepository) : base(repository, unitOfWork)
        {
            _mapper = mapper;
            _plantRepository = plantRepository;
            _unitOfWork = unitOfWork;

        }

        public async Task<IEnumerable<PlantListDto>> GetByCompanyIdPlants(Guid companyId)
        {
            var values= await _plantRepository.Where(x=>x.CompanyId==companyId).Include("Company").ToListAsync();
            return values.Select(x => new PlantListDto 
            {
                CompanyName=x.Company.Name,
                Name=x.Name,
                CreatedDate=x.CreatedDate,
                Id=x.Id,
            }
            ).ToList();
        }

        public async Task<IEnumerable<PlantListDto>> GetByCompanyNamePlants(string companyName)
        {
            var values = await _plantRepository.Where(x => x.Company.Name.ToLower().Contains(companyName.ToLower())).Include("Company").ToListAsync();
            return values.Select(y => new PlantListDto
            {
                CompanyName = y.Company.Name,
                Name=y.Name,
                CreatedDate = y.CreatedDate,
                Id=y.Id,
            }).ToList();
        }

        public async Task<PlantReturnId> GetByPlantNameReturnId(string plantName)
        {
            var value= await _plantRepository.Where(x=>x.Name.ToLower()==plantName.ToLower()).FirstOrDefaultAsync();
            if (value == null)
            {
                return new PlantReturnId { PlantId = Guid.Empty };
            }
            else
            {
                return new PlantReturnId { PlantId = value.Id };
            }
        }

        public async Task<IEnumerable<PlantListCompanyName>> GetPlantListByCompanyName(string companyName)
        {
            var values = await _plantRepository.Where(x => x.Company.Name.ToLower() == companyName.ToLower()).Include("Company").ToListAsync();
            return values.Select(y=>new PlantListCompanyName { PlantName=y.Name }).ToList();
        }

        public async Task<List<PlantListDto>> GetPlants()
        {
            var plants = await _plantRepository.GetAll().Include("Company").ToListAsync();
            return plants.Select(x =>
            new PlantListDto
            {
                CompanyName=x.Company.Name,
                CreatedDate=x.CreatedDate,
                Name=x.Name,
                Id = x.Id
            }).ToList();
        }
    }
}
