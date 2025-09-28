using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using vesa.core.Models.CRM;

namespace vesa.core.Repositories
{
	public interface ICustomerPhoneRepository : IGenericRepository<CustomerPhone>
	{
		Task<CustomerPhone> GetDetailAsync(Guid id);
		Task<List<CustomerPhone>> GetByCustomerIdAsync(Guid customerId);
		uint GetConcurrencyToken(CustomerPhone entity);
	}
}
