using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using formneo.core.DTOs.UserTenants;
using formneo.core.Models;
using NLayer.Core.Services;

namespace formneo.core.Services
{
    public interface IUserTenantService : IGlobalServiceWithDto<UserTenant, UserTenantListDto>
    {
        Task<UserTenantListDto> AddAsync(UserTenantInsertDto dto);
        Task<UserTenantListDto> UpdateAsync(UserTenantUpdateDto dto);
        Task<UserTenantListDto> GetByUserAndTenantAsync(string userId, Guid tenantId);
        Task<IEnumerable<UserTenantFullDto>> GetByUserAsync(string userId);
        Task<IEnumerable<UserTenantListDto>> GetByTenantAsync(Guid tenantId);
        Task<IEnumerable<UserTenantByTenantDto>> GetUsersByTenantAsync(Guid tenantId);
        Task<IEnumerable<UserTenantFullDto>> GetAllFullAsync();
        Task BulkAssignUsersAsync(UserTenantBulkAssignUsersDto dto);
    }
}


