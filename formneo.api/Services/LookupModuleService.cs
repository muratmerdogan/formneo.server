using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using vesa.core.DTOs.Lookup;
using vesa.core.Models.Lookup;
using vesa.core.Repositories;
using vesa.core.Services;
using vesa.core.UnitOfWorks;

namespace vesa.service.Services
{
    public class LookupModuleService : ILookupModuleService
    {
        private readonly ILookupModuleRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public LookupModuleService(ILookupModuleRepository repository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<LookupModuleDto>> GetAllAsync()
        {
            var list = await _repository.GetAll().ToListAsync();
            return _mapper.Map<List<LookupModuleDto>>(list);
        }

        public async Task<LookupModuleDto?> GetByIdAsync(Guid id)
        {
            var entity = await _repository.GetByIdStringGuidAsync(id);
            return entity == null ? null : _mapper.Map<LookupModuleDto>(entity);
        }

        public async Task<LookupModuleDto> CreateAsync(LookupModuleDto dto)
        {
            var entity = _mapper.Map<LookupModule>(dto);
            await _repository.AddAsync(entity);
            await _unitOfWork.CommitAsync();
            return _mapper.Map<LookupModuleDto>(entity);
        }

        public async Task<LookupModuleDto?> UpdateAsync(Guid id, LookupModuleDto dto)
        {
            var entity = await _repository.GetByIdStringGuidAsync(id);
            if (entity == null) return null;
            _mapper.Map(dto, entity);
            _repository.Update(entity);
            await _unitOfWork.CommitAsync();
            return _mapper.Map<LookupModuleDto>(entity);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var entity = await _repository.GetByIdStringGuidAsync(id);
            if (entity == null) return false;
            _repository.Remove(entity);
            await _unitOfWork.CommitAsync();
            return true;
        }
    }
}


