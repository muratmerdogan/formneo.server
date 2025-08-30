using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using vesa.core.DTOs.CRM;
using vesa.core.Services;

namespace vesa.api.Controllers.CRM
{
	[Route("api/[controller]")]
	[ApiController]
	public class CustomerNotesController : ControllerBase
	{
		private readonly ICustomerNoteService _customerNoteService;

		public CustomerNotesController(ICustomerNoteService customerNoteService)
		{
			_customerNoteService = customerNoteService;
		}

		[HttpGet]
		public async Task<IActionResult> GetList()
		{
			var data = await _customerNoteService.GetListAsync();
			return Ok(data);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetById(Guid id)
		{
			var data = await _customerNoteService.GetByIdAsync(id);
			if (data == null) return NotFound();
			return Ok(data);
		}

		[HttpGet("customer/{customerId}")]
		public async Task<IActionResult> GetByCustomerId(Guid customerId)
		{
			var data = await _customerNoteService.GetByCustomerIdAsync(customerId);
			return Ok(data);
		}

		[HttpPost]
		public async Task<IActionResult> Create([FromBody] CustomerNoteInsertDto dto)
		{
			var created = await _customerNoteService.CreateAsync(dto);
			return Ok(created);
		}

		[HttpPut]
		public async Task<IActionResult> Update([FromBody] CustomerNoteUpdateDto dto)
		{
			var updated = await _customerNoteService.UpdateAsync(dto);
			if (updated == null) return NotFound();
			return Ok(updated);
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(Guid id)
		{
			await _customerNoteService.DeleteAsync(id);
			return NoContent();
		}
	}
}
