using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using vesa.core.DTOs.Lookup;
using vesa.core.Services;

namespace vesa.api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class LookupModuleController : CustomBaseController
	{
		private readonly ILookupModuleService _service;

		public LookupModuleController(ILookupModuleService service)
		{
			_service = service;
		}

		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			var list = await _service.GetAllAsync();
			return Ok(list);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetById(Guid id)
		{
			var item = await _service.GetByIdAsync(id);
			if (item == null) return NotFound();
			return Ok(item);
		}

		[HttpPost]
		public async Task<IActionResult> Create([FromBody] LookupModuleDto dto)
		{
			var created = await _service.CreateAsync(dto);
			return Ok(created);
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> Update(Guid id, [FromBody] LookupModuleDto dto)
		{
			var updated = await _service.UpdateAsync(id, dto);
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


