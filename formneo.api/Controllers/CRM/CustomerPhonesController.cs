using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using vesa.api.Helper;
using vesa.core.DTOs.CRM;
using vesa.core.Services;
using vesa.service.Exceptions;

namespace vesa.api.Controllers.CRM
{
	[Route("api/customers/{customerId}/phones")]
	[ApiController]
	public class CustomerPhonesController : ControllerBase
	{
		private readonly ICustomerPhoneService _customerPhoneService;

		public CustomerPhonesController(ICustomerPhoneService customerPhoneService)
		{
			_customerPhoneService = customerPhoneService;
		}

		[HttpGet]
		public async Task<IActionResult> GetByCustomerId(Guid customerId)
		{
			var phones = await _customerPhoneService.GetByCustomerIdAsync(customerId);
			return Ok(phones);
		}

		[HttpGet("{phoneId}")]
		public async Task<IActionResult> GetById(Guid customerId, Guid phoneId)
		{
			var phone = await _customerPhoneService.GetByIdAsync(phoneId);
			if (phone == null || phone.CustomerId != customerId)
				return NotFound();
			return Ok(phone);
		}

		[HttpPost]
		public async Task<IActionResult> Create(Guid customerId, [FromBody] CustomerPhoneInsertDto dto)
		{
			dto.CustomerId = customerId;
			if (!ValidationHelper.IsValidOrReturnError(ModelState, out var validationResult))
				return validationResult;

			var created = await _customerPhoneService.CreateAsync(dto);
			return CreatedAtAction(nameof(GetById), new { customerId, phoneId = created.Id }, created);
		}

		[HttpPut("{phoneId}")]
		public async Task<IActionResult> Update(Guid customerId, Guid phoneId, [FromBody] CustomerPhoneUpdateDto dto)
		{
			dto.Id = phoneId;
			dto.CustomerId = customerId;
			if (!ValidationHelper.IsValidOrReturnError(ModelState, out var validationResult))
				return validationResult;

			try
			{
				var updated = await _customerPhoneService.UpdateAsync(dto);
				if (updated == null) return NotFound();
				return Ok(updated);
			}
			catch (ClientSideException ex)
			{
				ModelState.AddModelError("Concurrency", ex.Message);
				return ValidationHelper.GetValidationErrorResponse(ModelState);
			}
		}

		[HttpDelete("{phoneId}")]
		public async Task<IActionResult> Delete(Guid customerId, Guid phoneId)
		{
			try
			{
				await _customerPhoneService.DeleteAsync(phoneId);
				return NoContent();
			}
			catch (ClientSideException ex)
			{
				ModelState.AddModelError("Concurrency", ex.Message);
				return ValidationHelper.GetValidationErrorResponse(ModelState);
			}
		}

		[HttpPut("{phoneId}/set-primary")]
		public async Task<IActionResult> SetPrimary(Guid customerId, Guid phoneId)
		{
			try
			{
				await _customerPhoneService.SetPrimaryAsync(customerId, phoneId);
				return NoContent();
			}
			catch (ClientSideException ex)
			{
				ModelState.AddModelError("Concurrency", ex.Message);
				return ValidationHelper.GetValidationErrorResponse(ModelState);
			}
		}
	}
}
