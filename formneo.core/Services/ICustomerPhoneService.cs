using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using vesa.core.DTOs.CRM;

namespace vesa.core.Services
{
	public interface ICustomerPhoneService
	{
		Task<CustomerPhoneDto> GetByIdAsync(Guid id);
		Task<IEnumerable<CustomerPhoneDto>> GetByCustomerIdAsync(Guid customerId);
		Task<CustomerPhoneDto> CreateAsync(CustomerPhoneInsertDto dto);
		Task<CustomerPhoneDto> UpdateAsync(CustomerPhoneUpdateDto dto);
		Task DeleteAsync(Guid id);
		Task SetPrimaryAsync(Guid customerId, Guid phoneId);
	}
}
