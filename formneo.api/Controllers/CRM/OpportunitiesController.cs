using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using formneo.core.DTOs.CRM;
using formneo.core.Services;

namespace formneo.api.Controllers.CRM
{
	[Route("api/crm/[controller]")]
	[ApiController]
	public class OpportunitiesController : ControllerBase
	{
		private readonly IOpportunityService _opportunityService;

		public OpportunitiesController(IOpportunityService opportunityService)
		{
			_opportunityService = opportunityService;
		}

		[HttpGet("customer/{customerId}")]
		public async Task<IActionResult> GetByCustomer(Guid customerId)
		{
			try
			{
				var opportunities = await _opportunityService.ListAsync(customerId);
				return Ok(opportunities);
			}
			catch (Exception ex)
			{
				return StatusCode(500, $"Internal error: {ex.Message}");
			}
		}

		[HttpGet("paged")]
		public async Task<IActionResult> GetListPaged(int page = 1, int pageSize = 50, string search = "", int? stage = null, Guid? customerId = null)
		{
			try
			{
				if (page < 1) page = 1;
				if (pageSize < 1 || pageSize > 100) pageSize = 50;

				var result = await _opportunityService.GetListPagedAsync(page, pageSize, search, stage, customerId);
				return Ok(result);
			}
			catch (Exception ex)
			{
				return StatusCode(500, $"Internal error: {ex.Message}");
			}
		}

		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			try
			{
				// Tüm opportunities için paged endpoint kullanılması önerilir
				var result = await _opportunityService.GetListPagedAsync(1, 1000);
				return Ok(result.Items);
			}
			catch (Exception ex)
			{
				return StatusCode(500, $"Internal error: {ex.Message}");
			}
		}

		[HttpGet("dashboard")]
		public async Task<IActionResult> GetDashboard(int months = 12, Guid? customerId = null)
		{
			try
			{
				if (months < 1 || months > 24) months = 12;

				var dashboard = await _opportunityService.GetDashboardAsync(months, customerId);
				return Ok(dashboard);
			}
			catch (Exception ex)
			{
				return StatusCode(500, $"Internal error: {ex.Message}");
			}
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetById(Guid id)
		{
			try
			{
				var opportunity = await _opportunityService.GetByIdAsync(id);
				if (opportunity == null) 
					return NotFound($"Opportunity with ID {id} not found");
				
				return Ok(opportunity);
			}
			catch (Exception ex)
			{
				return StatusCode(500, $"Internal error: {ex.Message}");
			}
		}

		[HttpPost]
		public async Task<IActionResult> Create([FromBody] OpportunityInsertDto dto)
		{
			try
			{
				if (!ModelState.IsValid)
					return BadRequest(ModelState);

				var created = await _opportunityService.CreateAsync(dto);
				return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
			}
			catch (Exception ex)
			{
				return StatusCode(500, $"Internal error: {ex.Message}");
			}
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> Update(Guid id, [FromBody] OpportunityUpdateDto dto)
		{
			try
			{
				if (!ModelState.IsValid)
					return BadRequest(ModelState);

				if (id != dto.Id)
					return BadRequest("ID mismatch");

				var updated = await _opportunityService.UpdateAsync(dto);
				if (updated == null) 
					return NotFound($"Opportunity with ID {id} not found");
				
				return Ok(updated);
			}
			catch (Exception ex)
			{
				return StatusCode(500, $"Internal error: {ex.Message}");
			}
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(Guid id)
		{
			try
			{
				await _opportunityService.DeleteAsync(id);
				return NoContent();
			}
			catch (Exception ex)
			{
				return StatusCode(500, $"Internal error: {ex.Message}");
			}
		}

		[HttpPost("{id}/stage")]
		public async Task<IActionResult> UpdateStage(Guid id, [FromBody] int stage)
		{
			try
			{
				if (!Enum.IsDefined(typeof(formneo.core.Models.CRM.OpportunityStage), stage))
					return BadRequest("Geçersiz stage");
				var updated = await _opportunityService.UpdateStageAsync(id, (formneo.core.Models.CRM.OpportunityStage)stage);
				if (updated == null) return NotFound($"Opportunity with ID {id} not found");
				return Ok(updated);
			}
			catch (Exception ex)
			{
				return StatusCode(500, $"Internal error: {ex.Message}");
			}
		}

		[HttpPost("{id}/won")]
		public async Task<IActionResult> MarkWon(Guid id)
		{
			try
			{
				var updated = await _opportunityService.MarkWonAsync(id);
				if (updated == null) return NotFound($"Opportunity with ID {id} not found");
				return Ok(updated);
			}
			catch (Exception ex)
			{
				return StatusCode(500, $"Internal error: {ex.Message}");
			}
		}

		[HttpPost("{id}/lost")]
		public async Task<IActionResult> MarkLost(Guid id)
		{
			try
			{
				var updated = await _opportunityService.MarkLostAsync(id);
				if (updated == null) return NotFound($"Opportunity with ID {id} not found");
				return Ok(updated);
			}
			catch (Exception ex)
			{
				return StatusCode(500, $"Internal error: {ex.Message}");
			}
		}
	}
}


