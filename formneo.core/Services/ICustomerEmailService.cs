using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using formneo.core.DTOs.CRM;

namespace formneo.core.Services
{
	public interface ICustomerEmailService
	{
		Task<CustomerEmailDto?> GetByIdAsync(Guid id);
		Task<IEnumerable<CustomerEmailDto>> GetByCustomerIdAsync(Guid customerId);
		Task<CustomerEmailDto> CreateAsync(CustomerEmailInsertDto dto);
		Task<CustomerEmailDto?> UpdateAsync(CustomerEmailUpdateDto dto);
		Task DeleteAsync(Guid id);
		Task SetPrimaryAsync(Guid customerId, Guid emailId);
	}
}
