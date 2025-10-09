using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using formneo.core.DTOs.TenantProject;
using formneo.core.Services;

namespace formneo.api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	public class TenantProjectsController : ControllerBase
	{
		private readonly ITenantProjectService _service;

		public TenantProjectsController(ITenantProjectService service)
		{
			_service = service;
		}

		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			var list = await _service.GetAllAsyncList();
			return Ok(list);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetById(Guid id)
		{
			var item = await _service.GetDetailAsync(id);
			if (item == null) return NotFound();
			return Ok(item);
		}

		[HttpPost]
		public async Task<IActionResult> Create([FromBody] TenantProjectInsertDto dto)
		{
			var created = await _service.CreateAsync(dto);
			return Ok(created);
		}

		[HttpPut]
		public async Task<IActionResult> Update([FromBody] TenantProjectUpdateDto dto)
		{
			var updated = await _service.UpdateAsync(dto);
			if (updated == null) return NotFound();
			return Ok(updated);
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(Guid id)
		{
			var ok = await _service.DeleteAsync(id);
			if (!ok) return NotFound();
			return NoContent();
		}
	}
}


