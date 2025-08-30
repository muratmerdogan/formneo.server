using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using vesa.core.DTOs;
using vesa.core.DTOs.Clients;
using vesa.core.Models;

namespace vesa.core.Services
{
    public interface IClientService
    {
        Task<CustomResponseDto<List<MainClientListDto>>> GetAllAsync();
        Task<CustomResponseDto<MainClientListDto>> GetByIdAsync(Guid id);
        Task<CustomResponseDto<MainClientListDto>> AddAsync(MainClientInsertDto dto);
        Task<CustomResponseDto<NoContentDto>> UpdateAsync(MainClientUpdateDto dto);
        Task<CustomResponseDto<NoContentDto>> RemoveAsync(Guid id);
        Task<CustomResponseDto<List<MainClientListDto>>> GetActiveClientsAsync();
        Task<Dictionary<Guid, int>> GetUserCountsByTenantAsync();
    }
}