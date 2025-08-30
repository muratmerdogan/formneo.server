using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using vesa.core.DTOs.CRM;
using vesa.core.Services;

namespace vesa.api.Controllers.CRM
{
	[Route("api/crm/[controller]")]
	[ApiController]
	public class OpportunitiesController : ControllerBase
	{
		private readonly IOpportunityService _service;
		public OpportunitiesController(IOpportunityService service){_service=service;}

		[HttpGet("customer/{customerId}")]
		public async Task<IActionResult> List(Guid customerId){var data=await _service.ListAsync(customerId);return Ok(data);}
		[HttpGet("{id}")]
		public async Task<IActionResult> GetById(Guid id){var data=await _service.GetByIdAsync(id);if(data==null)return NotFound();return Ok(data);}
		[HttpPost]
		public async Task<IActionResult> Create([FromBody] OpportunityDto dto){var created=await _service.CreateAsync(dto);return Ok(created);}
		[HttpPut]
		public async Task<IActionResult> Update([FromBody] OpportunityDto dto){var updated=await _service.UpdateAsync(dto);if(updated==null)return NotFound();return Ok(updated);}
		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(Guid id){await _service.DeleteAsync(id);return NoContent();}
	}
}


