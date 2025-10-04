using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using formneo.core.DTOs.CRM;

namespace formneo.core.Services
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
