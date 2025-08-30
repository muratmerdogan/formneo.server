using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vesa.core.DTOs.Clients;
using vesa.core.Models;
using vesa.core.Repositories;
using vesa.core.Services;
using vesa.core.UnitOfWorks;
using vesa.repository;
using Microsoft.EntityFrameworkCore;

namespace vesa.service.Services
{
    public class ClientService: Service<MainClient>, IClientService
    {
        private readonly IClientRepository _clientRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ClientService(IGenericRepository<MainClient> repository, IUnitOfWork unitOfWork, IMapper mapper, IClientRepository clientRepository) : base(repository, unitOfWork)
        {
            _mapper = mapper;
            _clientRepository = clientRepository;
            _unitOfWork = unitOfWork;

        }

        public async Task<List<MainClientListDto>> GetByClientName(string clientName)
        {
            var values = await _clientRepository.Where(x => x.Name.ToLower().Contains(clientName.ToLower())).ToListAsync();
            var dto=_mapper.Map<List<MainClientListDto>>(values);
            return dto;

        }

        public async Task<ClientReturnGuidId> GetClientReturnGuidId(string clientName)
        {
            var values = await _clientRepository.Where(x => x.Name.ToLower() == clientName.ToLower()).FirstOrDefaultAsync();
            if (values == null)
            {
                return new ClientReturnGuidId { ClientId=Guid.Empty};
            }
            else
            {
                return new ClientReturnGuidId { ClientId = values.Id };
            }
        }

        public async Task<Dictionary<Guid, int>> GetUserCountsByTenantAsync()
        {
            // Resolve repository via context to avoid circular deps; using context directly for aggregation
            var context = (AppDbContext)((vesa.repository.Repositories.GenericRepository<MainClient>)_clientRepository).GetType()
                .GetField("_context", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                ?.GetValue(_clientRepository);

            if (context == null)
            {
                return new Dictionary<Guid, int>();
            }

            var counts = await context.Set<UserTenant>()
                .AsNoTracking()
                .GroupBy(x => x.TenantId)
                .Select(g => new { TenantId = g.Key, Count = g.Count() })
                .ToDictionaryAsync(k => k.TenantId, v => v.Count);
            return counts;
        }
    }
}
