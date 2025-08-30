using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using vesa.core.DTOs.CRM;

namespace vesa.core.Services
{
	public interface ICustomerAddressService
	{
		Task<CustomerAddressDto> GetByIdAsync(Guid id);
		Task<IEnumerable<CustomerAddressDto>> GetByCustomerIdAsync(Guid customerId);
		Task<CustomerAddressDto> CreateAsync(CustomerAddressInsertDto dto);
		Task<CustomerAddressDto> UpdateAsync(CustomerAddressUpdateDto dto);
		Task DeleteAsync(Guid id);
		Task SetDefaultBillingAsync(Guid customerId, Guid addressId);
		Task SetDefaultShippingAsync(Guid customerId, Guid addressId);
	}
}
