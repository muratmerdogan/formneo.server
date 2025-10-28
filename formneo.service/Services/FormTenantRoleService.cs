using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using formneo.core.DTOs.RoleForm;
using formneo.core.Models;
using formneo.core.Repositories;
using formneo.core.Services;
using formneo.core.UnitOfWorks;
using NLayer.Service.Services;

namespace formneo.service.Services
{
    public class FormTenantRoleService : ServiceWithDto<FormTenantRole, FormTenantRoleListDto>, IFormTenantRoleService
    {
        private readonly IGenericRepository<FormTenantRole> _repo;

        public FormTenantRoleService(IGenericRepository<FormTenantRole> repo, IUnitOfWork unitOfWork, IMapper mapper) : base(repo, unitOfWork, mapper)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<FormTenantRoleListDto>> GetAllAsync()
        {
            var all = await _repo.GetAllAsync();
            return _mapper.Map<IEnumerable<FormTenantRoleListDto>>(all);
        }
    }
}


