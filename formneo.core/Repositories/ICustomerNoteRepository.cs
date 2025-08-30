using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using vesa.core.Models.CRM;

namespace vesa.core.Repositories
{
	public interface ICustomerNoteRepository : IGenericRepository<CustomerNote>
	{
		Task<List<CustomerNote>> GetByCustomerIdAsync(Guid customerId);
		Task<CustomerNote> GetDetailAsync(Guid id);
	}
}
