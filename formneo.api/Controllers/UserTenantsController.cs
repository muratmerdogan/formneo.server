using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using vesa.core.DTOs;
using vesa.core.DTOs.UserTenants;
using vesa.core.Models;
using vesa.core.Services;

namespace vesa.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class UserTenantsController : CustomBaseController
    {
        private readonly IUserTenantService _service;
        private readonly IUserTenantRoleService _userTenantRoleService;
        private readonly IConfiguration _configuration;

        public UserTenantsController(IUserTenantService service, IUserTenantRoleService userTenantRoleService, IConfiguration configuration)
        {
			_service = service;
            _userTenantRoleService = userTenantRoleService;
            _configuration = configuration;
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
			var list = await _service.GetByUserAsync(userId);
			var tenantAdminRoleId = _configuration.GetValue<string>("RoleScope:TenantAdminRoleId") ?? "7f3d3baf-2f5c-4f6c-9d1e-6b6d3b25a001";
			var result = new List<UserTenantWithAdminFlagDto>();
			foreach (var item in list)
			{
				// item'in TenantId alanı olduğu varsayımıyla
				var tenantIdProp = item.GetType().GetProperty("TenantId");
				Guid tenantId = Guid.Empty;
				if (tenantIdProp != null)
				{
					var val = tenantIdProp.GetValue(item);
					if (val is Guid g) tenantId = g;
				}

				bool isTenantAdmin = false;
				if (tenantId != Guid.Empty && !string.IsNullOrWhiteSpace(userId))
				{
					var roles = await _userTenantRoleService.GetByUserAndTenantAsync(userId, tenantId);
					if (roles != null)
					{
						isTenantAdmin = roles.Any(r => r?.RoleTenant?.RoleId == tenantAdminRoleId);
					}
				}

                var dto = UserTenantWithAdminFlagDto.From(item, isTenantAdmin);
				result.Add(dto);
			}
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


