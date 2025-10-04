using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using formneo.api.Helper;
using formneo.core.DTOs.CRM;
using formneo.core.Services;
using formneo.service.Exceptions;

namespace formneo.api.Controllers.CRM
{
	[Route("api/customers/{customerId}/emails")]
	[ApiController]
	public class CustomerEmailsController : ControllerBase
	{
		private readonly ICustomerEmailService _customerEmailService;

		public CustomerEmailsController(ICustomerEmailService customerEmailService)
		{
			_customerEmailService = customerEmailService;
		}

		[HttpGet]
		public async Task<IActionResult> GetByCustomerId(Guid customerId)
		{
			var emails = await _customerEmailService.GetByCustomerIdAsync(customerId);
			return Ok(emails);
		}

		[HttpGet("{emailId}")]
		public async Task<IActionResult> GetById(Guid customerId, Guid emailId)
		{
			var email = await _customerEmailService.GetByIdAsync(emailId);
			if (email == null || email.CustomerId != customerId)
				return NotFound();
			return Ok(email);
		}

		[HttpPost]
		public async Task<IActionResult> Create(Guid customerId, [FromBody] CustomerEmailInsertDto dto)
		{
			dto.CustomerId = customerId;
			if (!ValidationHelper.IsValidOrReturnError(ModelState, out var validationResult))
				return validationResult;

			var created = await _customerEmailService.CreateAsync(dto);
			return CreatedAtAction(nameof(GetById), new { customerId, emailId = created.Id }, created);
		}

		[HttpPut("{emailId}")]
		public async Task<IActionResult> Update(Guid customerId, Guid emailId, [FromBody] CustomerEmailUpdateDto dto)
		{
			dto.Id = emailId;
			dto.CustomerId = customerId;
			if (!ValidationHelper.IsValidOrReturnError(ModelState, out var validationResult))
				return validationResult;

			try
			{
				var updated = await _customerEmailService.UpdateAsync(dto);
				if (updated == null) return NotFound();
				return Ok(updated);
			}
			catch (ClientSideException ex)
			{
				ModelState.AddModelError("Concurrency", ex.Message);
				return ValidationHelper.GetValidationErrorResponse(ModelState);
			}
		}

		[HttpDelete("{emailId}")]
		public async Task<IActionResult> Delete(Guid customerId, Guid emailId)
		{
			try
			{
				await _customerEmailService.DeleteAsync(emailId);
				return NoContent();
			}
			catch (ClientSideException ex)
			{
				ModelState.AddModelError("Concurrency", ex.Message);
				return ValidationHelper.GetValidationErrorResponse(ModelState);
			}
		}

		[HttpPut("{emailId}/set-primary")]
		public async Task<IActionResult> SetPrimary(Guid customerId, Guid emailId)
		{
			try
			{
				await _customerEmailService.SetPrimaryAsync(customerId, emailId);
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
