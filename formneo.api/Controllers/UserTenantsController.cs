using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using formneo.core.DTOs;
using formneo.core.DTOs.UserTenants;
using formneo.core.Models;
using formneo.core.Services;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.EntityFrameworkCore;

namespace formneo.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class UserTenantsController : CustomBaseController
    {
        private readonly IUserTenantService _service;
        private readonly IUserTenantRoleService _userTenantRoleService;
        private readonly IConfiguration _configuration;
        private readonly IMemoryCache _memoryCache;

        public UserTenantsController(IUserTenantService service, IUserTenantRoleService userTenantRoleService, IConfiguration configuration, IMemoryCache memoryCache)
        {
			_service = service;
            _userTenantRoleService = userTenantRoleService;
            _configuration = configuration;
            _memoryCache = memoryCache;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var result = await _service.GetAllAsync();
            return CreateActionResult(result);
        }

        // Tam bağlı (Include) liste: Kullanıcı adı-soyadı, email ve tenant adı/slug ile birlikte
        [HttpGet("full")]
        public async Task<IActionResult> AllFull()
        {
            var list = await _service.GetAllFullAsync();
            return Ok(list);
        }

	[HttpGet("by-user/{userId}")]
        public async Task<ActionResult<IEnumerable<UserTenantWithAdminFlagDto>>> GetByUser(string userId)
	{
		// Hız için sonuçları kısa süreli cache'le (örn. 2 dk)
		var cacheKey = $"userTenants:{userId}";
		if (_memoryCache.TryGetValue(cacheKey, out List<UserTenantWithAdminFlagDto> cached))
		{
			return Ok(cached);
		}

		var list = await _service.GetByUserAsync(userId);
		var tenantAdminRoleId = _configuration.GetValue<string>("RoleScope:TenantAdminRoleId") ?? "7f3d3baf-2f5c-4f6c-9d1e-6b6d3b25a001";
		var result = new List<UserTenantWithAdminFlagDto>();

		// Tüm tenantId'leri tek seferde topla
		var tenantIds = new HashSet<Guid>();
		foreach (var item in list)
		{
			var tenantIdProp = item.GetType().GetProperty("TenantId");
			if (tenantIdProp != null)
			{
				var val = tenantIdProp.GetValue(item);
				if (val is Guid g && g != Guid.Empty)
				{
					tenantIds.Add(g);
				}
			}
		}

		// Kullanıcının bu tenant'lardaki aktif rollerini TEK sorguda çek
		var rolesByTenant = await _userTenantRoleService
			.Where(x => x.UserId == userId && tenantIds.Contains(x.RoleTenant.TenantId) && x.IsActive && x.RoleTenant.IsActive)
			.Select(x => new { x.RoleTenant.TenantId, x.RoleTenant.RoleId })
			.AsNoTracking()
			.ToListAsync();

		var adminTenants = rolesByTenant
			.GroupBy(r => r.TenantId)
			.Where(g => g.Any(r => r.RoleId == tenantAdminRoleId))
			.Select(g => g.Key)
			.ToHashSet();
		foreach (var item in list)
		{
			var ut = item as formneo.core.DTOs.UserTenants.UserTenantFullDto;
			Guid tenantId = ut?.TenantId ?? Guid.Empty;

			bool isTenantAdmin = tenantId != Guid.Empty && adminTenants.Contains(tenantId);

			var dto = new UserTenantWithAdminFlagDto
			{
				Id = ut?.Id ?? Guid.Empty,
				UserId = ut?.UserId ?? string.Empty,
				TenantId = ut?.TenantId ?? Guid.Empty,
				IsActive = ut?.IsActive ?? false,
				CreatedDate = default,
				UpdatedDate = null,
				TenantName = ut?.TenantName ?? string.Empty,
				TenantSlug = ut?.TenantSlug ?? string.Empty,
				IsTenantAdmin = isTenantAdmin
			};
			
			result.Add(dto);
		}
		// Cache'e yaz
		_memoryCache.Set(cacheKey, result, new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(2)));
		return Ok(result);
	}

        [HttpGet("by-tenant/{tenantId}")]
        public async Task<List<UserTenantByTenantDto>> GetByTenant(Guid tenantId)
        {
            var list = await _service.GetUsersByTenantAsync(tenantId);
            return list.ToList();
        }

        [HttpGet("by/{userId}/{tenantId}")]
        public async Task<IActionResult> GetByUserAndTenant(string userId, Guid tenantId)
        {
            var item = await _service.GetByUserAndTenantAsync(userId, tenantId);
            return Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> Create(UserTenantInsertDto dto)
        {
            var item = await _service.AddAsync(dto);
            return Ok(item);
        }

        [HttpPut]
        public async Task<IActionResult> Update(UserTenantUpdateDto dto)
        {
            var item = await _service.UpdateAsync(dto);
            return Ok(item);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _service.RemoveAsyncByGuid(id);
            return CreateActionResult(result);
        }

        // Sadece: tek tenant için çoklu user atama

        // Tek tenant, birden fazla kullanıcı (temizle + ekle)
        [HttpPost("bulk/assign-users")]
        public async Task<IActionResult> BulkAssignUsers(UserTenantBulkAssignUsersDto dto)
        {
            await _service.BulkAssignUsersAsync(dto);
            return Ok();
        }
    }
}


