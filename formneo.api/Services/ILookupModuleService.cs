using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using formneo.core.DTOs.Lookup;

namespace formneo.core.Services
{
    public interface ILookupModuleService
    {
        Task<List<LookupModuleDto>> GetAllAsync();
        Task<LookupModuleDto?> GetByIdAsync(Guid id);
        Task<LookupModuleDto> CreateAsync(LookupModuleDto dto);
        Task<LookupModuleDto?> UpdateAsync(Guid id, LookupModuleDto dto);
        Task<bool> DeleteAsync(Guid id);
    }
}


