using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using vesa.core.DTOs.CRM;

namespace vesa.core.Services
{
	public interface ICustomerNoteService
	{
		Task<CustomerNoteDto> GetByIdAsync(Guid id);
		Task<IEnumerable<CustomerNoteDto>> GetListAsync();
		Task<IEnumerable<CustomerNoteDto>> GetByCustomerIdAsync(Guid customerId);
		Task<CustomerNoteDto> CreateAsync(CustomerNoteInsertDto dto);
		Task<CustomerNoteDto> UpdateAsync(CustomerNoteUpdateDto dto);
		Task DeleteAsync(Guid id);
	}
}
