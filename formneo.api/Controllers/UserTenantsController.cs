using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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

        public UserTenantsController(IUserTenantService service)
        {
            _service = service;
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
        public async Task<ActionResult<IEnumerable<UserTenantFullDto>>> GetByUser(string userId)
        {
            var list = await _service.GetByUserAsync(userId);
            return Ok(list);
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


