using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using formneo.core.DTOs.RoleTenants;
using formneo.core.Models;
using NLayer.Core.Services;
using formneo.core.DTOs;
using formneo.core.DTOs.RoleTenants;

namespace formneo.core.Services
{
	public interface IRoleTenantService : IGlobalServiceWithDto<RoleTenant, RoleTenantListDto>
	{
		Task<RoleTenantListDto> AddAsync(RoleTenantInsertDto dto);
		Task<RoleTenantListDto> UpdateAsync(RoleTenantUpdateDto dto);
		Task<RoleTenantListDto> GetByRoleAndTenantAsync(string roleId, Guid tenantId);
		Task<IEnumerable<RoleTenantListDto>> GetByTenantAsync(Guid tenantId);
		Task<IEnumerable<RoleTenantListDto>> GetByRoleAsync(string roleId);
		Task RemoveAllByRoleAsync(string roleId);
		Task RemoveAllByTenantAsync(Guid tenantId);
		Task RemoveByRoleAndTenantAsync(string roleId, Guid tenantId);
		Task BulkSaveAsync(RoleTenantBulkSaveDto dto);

		// Yeni: Tenant bazında roller ve menüleri toplu sil-kaydet
		Task BulkSaveWithMenusAsync(RoleTenantWithMenusBulkSaveDto dto);
		// Yeni: Tenant bazında roller + menü yapısını döndür
		Task<RoleTenantWithMenusGetDto> GetByTenantWithMenusAsync(Guid tenantId);

		// Yeni: Kullanıcı rol atama işlemleri
		Task<UserRoleAssignmentGetDto> GetUserRoleAssignmentsAsync(string userId, Guid tenantId);
		Task SaveUserRoleAssignmentsAsync(UserRoleAssignmentSaveDto dto);
	}
}



