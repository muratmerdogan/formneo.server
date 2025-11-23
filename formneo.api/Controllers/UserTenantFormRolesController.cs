using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using formneo.core.DTOs.RoleForm;
using formneo.core.Services;

namespace formneo.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class UserTenantFormRolesController : CustomBaseController
    {
        private readonly IUserTenantFormRoleService _service;

        public UserTenantFormRolesController(IUserTenantFormRoleService service)
        {
            _service = service;
        }

        // Tenant header'a göre, kullanıcının atanmış FormTenantRole'larını döner
        [HttpGet("by/{userId}")]
        public async Task<ActionResult<IEnumerable<UserTenantFormRoleListDto>>> GetByUser(string userId)
        {
            var list = await _service.GetByUserAsync(userId);
            return Ok(list);
        }

        // Tenant header'a göre, kullanıcının form rol atamalarını tamamen tazeler
        [HttpPost("bulk-save")]
        public async Task<IActionResult> BulkSave([FromBody] UserTenantFormRoleBulkSaveDto dto)
        {
            if (dto == null || string.IsNullOrWhiteSpace(dto.UserId))
            {
                return BadRequest("UserId zorunludur");
            }
            await _service.BulkSaveAsync(dto);
            return Ok();
        }
    }
}



