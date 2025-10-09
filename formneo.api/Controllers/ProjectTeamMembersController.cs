using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using formneo.core.DTOs.ProjectTeamMember;
using formneo.core.Services;

namespace formneo.api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	public class ProjectTeamMembersController : ControllerBase
	{
		private readonly IProjectTeamMemberService _service;

		public ProjectTeamMembersController(IProjectTeamMemberService service)
		{
			_service = service;
		}

		[HttpGet("by-project/{projectId}")]
		public async Task<IActionResult> GetByProject(Guid projectId)
		{
			var list = await _service.GetByProjectAsync(projectId);
			return Ok(list);
		}

		[HttpPost]
		public async Task<IActionResult> Create([FromBody] ProjectTeamMemberInsertDto dto)
		{
			var created = await _service.CreateAsync(dto);
			return Ok(created);
		}

		[HttpPut]
		public async Task<IActionResult> Update([FromBody] ProjectTeamMemberUpdateDto dto)
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


