using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using vesa.core.Models;
using vesa.core.Services;

namespace vesa.api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UserTenantRolesController : ControllerBase
	{
		private readonly IUserTenantRoleService _service;

		public UserTenantRolesController(IUserTenantRoleService service)
		{
			_service = service;
		}

		// GET: api/UserTenantRoles/by/{userId}/{tenantId}
		[HttpGet("by/{userId}/{tenantId}")]
		public async Task<ActionResult<IEnumerable<object>>> GetByUserAndTenant(string userId, Guid tenantId)
		{
			var list = await _service.GetByUserAndTenantAsync(userId, tenantId);
			var simplified = list.Select(x => new
			{
				UserTenantRoleId = x.Id,
				UserId = x.UserId,
				TenantId = x.RoleTenant.TenantId,
				RoleTenantId = x.RoleTenantId,
				RoleId = x.RoleTenant.RoleId,
				IsActive = x.IsActive
			});
			return Ok(simplified);
		}
	}
}


