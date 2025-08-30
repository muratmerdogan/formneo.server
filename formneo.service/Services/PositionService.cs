using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vesa.core.DTOs.PositionsDtos;
using vesa.core.Models;
using vesa.core.Repositories;
using vesa.core.Services;
using vesa.core.UnitOfWorks;

namespace vesa.service.Services
{
    public class PositionService: Service<Positions>, IPositionService
    {
        private readonly IPositionRepository _positionRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PositionService(IGenericRepository<Positions> repository, IUnitOfWork unitOfWork, IMapper mapper, IPositionRepository positionRepository) : base(repository, unitOfWork)
        {
            _mapper = mapper;
            _positionRepository = positionRepository;
            _unitOfWork = unitOfWork;

        }

        public async Task<PositionListDto> GetByIdPositionAsync(Guid id)
        {
            var value = await _positionRepository.GetByIdStringGuidAsync(id);
            return value != null ? _mapper.Map<PositionListDto>(value) : new PositionListDto();
        }

        public async Task<IEnumerable<PositionListDto>> GetPositionsListAsync()
        {
            var values = await _positionRepository.GetAll().ToListAsync();
            return values!=null ? _mapper.Map<IEnumerable<PositionListDto>>(values) : new List<PositionListDto>();  
        }
    }
}
