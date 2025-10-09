using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using formneo.core.DTOs.ProjectTask;
using formneo.core.Services;

namespace formneo.api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	public class ProjectTaskItemsController : ControllerBase
	{
		private readonly IProjectTaskService _service;

		public ProjectTaskItemsController(IProjectTaskService service)
		{
			_service = service;
		}

		[HttpGet("by-project/{projectId}")]
		public async Task<IActionResult> GetByProject(Guid projectId)
		{
			var list = await _service.GetByProjectAsync(projectId);
			return Ok(list);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetDetail(Guid id)
		{
			var item = await _service.GetDetailAsync(id);
			if (item == null) return NotFound();
			return Ok(item);
		}

		[HttpPost]
		public async Task<IActionResult> Create([FromBody] ProjectTaskInsertDto dto)
		{
			var created = await _service.CreateAsync(dto);
			return Ok(created);
		}

		[HttpPut]
		public async Task<IActionResult> Update([FromBody] ProjectTaskUpdateDto dto)
		{
			var updated = await _service.UpdateAsync(dto);
			if (updated == null) return NotFound();
			return Ok(updated);
		}

		[HttpPatch("status")]
		public async Task<IActionResult> UpdateStatus([FromBody] ProjectTaskStatusUpdateDto dto)
		{
			var updated = await _service.UpdateStatusAsync(dto.Id, dto.Status);
			if (updated == null) return NotFound();
			return Ok(updated);
		}

		[HttpGet("{id}/history")]
		public async Task<IActionResult> GetHistory(Guid id)
		{
			var logs = await _service.GetHistoryAsync(id);
			return Ok(logs);
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
