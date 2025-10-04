using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using formneo.core.DTOs.Company;
using formneo.core.Models;
using formneo.core.Repositories;
using formneo.core.Services;
using formneo.core.UnitOfWorks;

namespace formneo.service.Services
{
    public class CompanyService : Service<Company>, ICompanyService
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CompanyService(IGenericRepository<Company> repository, IUnitOfWork unitOfWork, IMapper mapper, ICompanyRepository companyRepository) : base(repository, unitOfWork)
        {
            _mapper = mapper;
            _companyRepository = companyRepository;
            _unitOfWork = unitOfWork;

        }

        public async Task<IEnumerable<CompanyListDto>> GetAllCompanyListWithClientName()
        {
            var dto = await _companyRepository.GetAll().Include("Client").ToListAsync();
            return dto.Select(x =>
            new CompanyListDto
            {
                ClientName = x.Client.Name,
                CreatedDate = x.CreatedDate,
                Name = x.Name,
                Id = x.Id,
            }).ToList();
        }

        public async Task<IEnumerable<CompanyListDto>> GetClientIdWithCompanyList(Guid clientId)
        {
            var values = await _companyRepository.Where(x => x.ClientId == clientId).Include("Client").Select(x=>new CompanyListDto
            {
                ClientName=x.Client.Name,
                CreatedDate = x.CreatedDate,
                Id = x.Id,
                Name=x.Name,
            }).ToListAsync();
            return values;
        }

        public async Task<IEnumerable<CompanyListDto>> GetClientNameWithCompanyList(string clientName)
        {
            var values = await _companyRepository.Where(x => x.Client.Name.ToLower().Contains(clientName.ToLower())).Include("Client").ToListAsync();
            return values.Select(y => new CompanyListDto 
            {
                ClientName=y.Client.Name,
                CreatedDate=y.CreatedDate,
                Id=y.Id,
                Name=y.Name,
            }).ToList();
        }

        public async Task<IEnumerable<CompanyNameListDto>> GetCompanyNameList(string clientName)
        {
            var values = await _companyRepository.Where(x => x.Client.Name.ToLower() == clientName.ToLower()).Include("Client").ToListAsync();
            return values.Select(y=>new CompanyNameListDto { CompanyName=y.Name }).ToList();
        }

        public async Task<CompanyReturnId> GetCompanyNameReturnId(string companyName)
        {
            var value = await _companyRepository.Where(x => x.Name.ToLower() == companyName.ToLower()).FirstOrDefaultAsync();
            if (value == null)
            {
                return new CompanyReturnId { CompanyId = Guid.Empty };
            }
            else
            {
                return new CompanyReturnId { CompanyId = value.Id };
            }
        }
    }
}
