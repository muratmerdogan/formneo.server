using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using formneo.core.DTOs.CRM;
using formneo.core.Services;

namespace formneo.api.Controllers.CRM
{
	[Route("api/crm/[controller]")]
	[ApiController]
	public class SpecialDaysController : ControllerBase
	{
		private readonly ISpecialDayService _service;
		public SpecialDaysController(ISpecialDayService service){_service=service;}

		[HttpGet("customer/{customerId}")]
		public async Task<IActionResult> List(Guid customerId){var data=await _service.ListAsync(customerId);return Ok(data);}
		[HttpGet("{id}")]
		public async Task<IActionResult> GetById(Guid id){var data=await _service.GetByIdAsync(id);if(data==null)return NotFound();return Ok(data);}
		[HttpPost]
		public async Task<IActionResult> Create([FromBody] SpecialDayDto dto){var created=await _service.CreateAsync(dto);return Ok(created);}
		[HttpPut]
		public async Task<IActionResult> Update([FromBody] SpecialDayDto dto){var updated=await _service.UpdateAsync(dto);if(updated==null)return NotFound();return Ok(updated);}
		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(Guid id){await _service.DeleteAsync(id);return NoContent();}
	}
}


