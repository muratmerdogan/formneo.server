using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using vesa.core.DTOs.CRM;
using vesa.core.Services;

namespace vesa.api.Controllers.CRM
{
	[Route("api/customers/{customerId}/addresses")]
	[ApiController]
	public class CustomerAddressesController : ControllerBase
	{
		private readonly ICustomerAddressService _customerAddressService;

		public CustomerAddressesController(ICustomerAddressService customerAddressService)
		{
			_customerAddressService = customerAddressService;
		}

		[HttpGet]
		public async Task<IActionResult> GetByCustomerId(Guid customerId)
		{
			var addresses = await _customerAddressService.GetByCustomerIdAsync(customerId);
			return Ok(addresses);
		}

		[HttpGet("{addressId}")]
		public async Task<IActionResult> GetById(Guid customerId, Guid addressId)
		{
			var address = await _customerAddressService.GetByIdAsync(addressId);
			if (address == null || address.CustomerId != customerId)
				return NotFound();
			return Ok(address);
		}

		[HttpPost]
		public async Task<IActionResult> Create(Guid customerId, [FromBody] CustomerAddressInsertDto dto)
		{
			dto.CustomerId = customerId;
			var created = await _customerAddressService.CreateAsync(dto);
			return CreatedAtAction(nameof(GetById), new { customerId, addressId = created.Id }, created);
		}

		[HttpPut("{addressId}")]
		public async Task<IActionResult> Update(Guid customerId, Guid addressId, [FromBody] CustomerAddressUpdateDto dto)
		{
			dto.Id = addressId;
			dto.CustomerId = customerId;
			var updated = await _customerAddressService.UpdateAsync(dto);
			if (updated == null) return NotFound();
			return Ok(updated);
		}

		[HttpDelete("{addressId}")]
		public async Task<IActionResult> Delete(Guid customerId, Guid addressId)
		{
			await _customerAddressService.DeleteAsync(addressId);
			return NoContent();
		}

		[HttpPut("{addressId}/set-default-billing")]
		public async Task<IActionResult> SetDefaultBilling(Guid customerId, Guid addressId)
		{
			await _customerAddressService.SetDefaultBillingAsync(customerId, addressId);
			return NoContent();
		}

		[HttpPut("{addressId}/set-default-shipping")]
		public async Task<IActionResult> SetDefaultShipping(Guid customerId, Guid addressId)
		{
			await _customerAddressService.SetDefaultShippingAsync(customerId, addressId);
			return NoContent();
		}
	}
}
