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
	public class CustomersController : ControllerBase
	{
		private readonly ICustomerService _customerService;

		public CustomersController(ICustomerService customerService)
		{
			_customerService = customerService;
		}

		[HttpGet]
		public async Task<IActionResult> GetList()
		{
			var data = await _customerService.GetListAsync();
			return Ok(data);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetById(Guid id)
		{
			var data = await _customerService.GetByIdAsync(id);
			if (data == null) return NotFound();
			return Ok(data);
		}

		[HttpPost]
		public async Task<IActionResult> Create([FromBody] CustomerInsertDto dto)
		{
			var created = await _customerService.CreateAsync(dto);
			return Ok(created);
		}

		[HttpPut]
		public async Task<IActionResult> Update([FromBody] CustomerUpdateDto dto)
		{
			var updated = await _customerService.UpdateAsync(dto);
			if (updated == null) return NotFound();
			return Ok(updated);
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(Guid id)
		{
			await _customerService.DeleteAsync(id);
			return NoContent();
		}
	}
}


