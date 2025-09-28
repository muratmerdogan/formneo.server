using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using vesa.core.DTOs.CRM;
using vesa.core.Services;
using vesa.api.Helper;
using vesa.service.Exceptions;

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

		// Optimize edilmiş liste metodu - sayfalama ve arama ile
		[HttpGet("paged")]
		public async Task<IActionResult> GetListPaged(int page = 1, int pageSize = 50, bool includeDetails = false, string search = "")
		{
			if (page < 1) page = 1;
			if (pageSize < 1 || pageSize > 100) pageSize = 50; // Maksimum 100 kayıt

			var result = await _customerService.GetListPagedAsync(page, pageSize, includeDetails, search);
			return Ok(result);
		}
		// Hızlı liste - sadece temel bilgiler
		[HttpGet("basic")]
		public async Task<IActionResult> GetListBasic(int skip = 0, int take = 50)
		{
			if (skip < 0) skip = 0;
			if (take < 1 || take > 100) take = 50;

			var data = await _customerService.GetListBasicAsync(skip, take);
			return Ok(data);
		}
		// Toplam kayıt sayısı
		[HttpGet("count")]
		public async Task<IActionResult> GetCount()
		{
			var count = await _customerService.GetTotalCountAsync();
			return Ok(new { totalCount = count });
		}
		[HttpGet("{id}")]
		public async Task<IActionResult> GetById2(Guid id)
		{
			var data = await _customerService.GetByIdAsync(id);
			if (data == null) return NotFound();
			return Ok(data);
		}
        //[HttpGet("{id}")]
        //public async Task<IActionResult> GetById2(Guid id)
        //{
        //    var data = await _customerService.GetByIdAsync(id);
        //    if (data == null) return NotFound();
        //    return Ok(data);
        //}
        [HttpPost]
		public async Task<IActionResult> Create([FromBody] CustomerInsertDto dto)
		{
			if (!ValidationHelper.IsValidOrReturnError(ModelState, out var validationResult))
				return validationResult;

			try
			{
				var created = await _customerService.CreateAsync(dto);
				return Ok(created);
			}
			catch (InvalidOperationException ex)
			{
				return ValidationHelper.GetCustomValidationError("Code", ex.Message);
			}
		}
		[HttpPut]
		public async Task<IActionResult> Update([FromBody] CustomerUpdateDto dto)
		{
			if (dto.ConcurrencyToken == 0)
			{
				ModelState.AddModelError(nameof(dto.ConcurrencyToken), "ConcurrencyToken alanı zorunludur.");
			}

			if (!ValidationHelper.IsValidOrReturnError(ModelState, out var validationResult))
			{
				return validationResult;
			}	

			try
			{
				var updated = await _customerService.UpdateAsync(dto);
				if (updated == null) return NotFound();
				return Ok(updated);
			}
			catch (ClientSideException ex)
			{
				ModelState.AddModelError("Concurrency", ex.Message);
				return ValidationHelper.GetValidationErrorResponse(ModelState);
			}
		}
		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(Guid id)
		{
			await _customerService.DeleteAsync(id);
			return NoContent();
		}
	}
}


