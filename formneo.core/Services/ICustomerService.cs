using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using vesa.core.DTOs.CRM;

namespace vesa.core.Services
{
	public interface ICustomerService
	{
		Task<CustomerListDto> GetByIdAsync(Guid id);
		Task<IEnumerable<CustomerListDto>> GetListAsync();
		Task<CustomerListDto> CreateAsync(CustomerInsertDto dto);
		Task<CustomerListDto> UpdateAsync(CustomerUpdateDto dto);
		Task DeleteAsync(Guid id);
	}
}
