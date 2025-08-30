using AutoMapper;
using Microsoft.EntityFrameworkCore;
using vesa.core.DTOs;
using vesa.core.DTOs.Clients;
using vesa.core.Models;
using vesa.core.Repositories;
using vesa.core.Services;
using vesa.core.UnitOfWorks;
using vesa.repository;

namespace vesa.service.Services
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly AppDbContext _context;

        public ClientService(IClientRepository repository, IUnitOfWork unitOfWork, IMapper mapper, AppDbContext context)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _context = context;
        }

        public async Task<CustomResponseDto<List<MainClientListDto>>> GetAllAsync()
        {
            var clients = await _repository.GetAll().ToListAsync();
            var clientDtos = _mapper.Map<List<MainClientListDto>>(clients);
            return CustomResponseDto<List<MainClientListDto>>.Success(200, clientDtos);
        }

        public async Task<CustomResponseDto<MainClientListDto>> GetByIdAsync(Guid id)
        {
            var client = await _repository.GetByIdStringGuidAsync(id);
            if (client == null)
                return CustomResponseDto<MainClientListDto>.Fail(404, "Client not found");

            var clientDto = _mapper.Map<MainClientListDto>(client);
            return CustomResponseDto<MainClientListDto>.Success(200, clientDto);
        }

        public async Task<CustomResponseDto<MainClientListDto>> AddAsync(MainClientInsertDto dto)
        {
            var client = _mapper.Map<MainClient>(dto);
            await _repository.AddAsync(client);
            await _unitOfWork.CommitAsync();

            var clientDto = _mapper.Map<MainClientListDto>(client);
            return CustomResponseDto<MainClientListDto>.Success(201, clientDto);
        }

        public async Task<CustomResponseDto<NoContentDto>> UpdateAsync(MainClientUpdateDto dto)
        {
            var existingClient = await _repository.GetByIdStringGuidAsync(dto.Id);
            if (existingClient == null)
                return CustomResponseDto<NoContentDto>.Fail(404, "Client not found");

            var client = _mapper.Map<MainClientUpdateDto, MainClient>(dto, existingClient);
            client.UpdatedDate = DateTime.Now;

            _repository.Update(client);
            await _unitOfWork.CommitAsync();

            return CustomResponseDto<NoContentDto>.Success(204);
        }
        public async Task<CustomResponseDto<NoContentDto>> RemoveAsync(Guid id)
        {
            var client = await _repository.GetByIdStringGuidAsync(id);
            if (client == null)
                return CustomResponseDto<NoContentDto>.Fail(404, "Client not found");

            _repository.Remove(client);
            await _unitOfWork.CommitAsync();

            return CustomResponseDto<NoContentDto>.Success(204);
        }
        public async Task<CustomResponseDto<List<MainClientListDto>>> GetActiveClientsAsync()
        {
            var clients = await _repository.GetActiveAsync();
            var clientDtos = _mapper.Map<List<MainClientListDto>>(clients);
            return CustomResponseDto<List<MainClientListDto>>.Success(200, clientDtos);
        }

        public async Task<Dictionary<Guid, int>> GetUserCountsByTenantAsync()
        {
            var counts = await _context.Set<UserTenant>()
                .AsNoTracking()
                .GroupBy(x => x.TenantId)
                .Select(g => new { TenantId = g.Key, Count = g.Count() })
                .ToDictionaryAsync(k => k.TenantId, v => v.Count);
            return counts;
        }
    }
}