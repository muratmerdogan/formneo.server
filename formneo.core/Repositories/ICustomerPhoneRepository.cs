using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using formneo.core.Models.CRM;

namespace formneo.core.Repositories
{
	public interface ICustomerPhoneRepository : IGenericRepository<CustomerPhone>
	{
		Task<CustomerPhone> GetDetailAsync(Guid id);
		Task<List<CustomerPhone>> GetByCustomerIdAsync(Guid customerId);
		uint GetConcurrencyToken(CustomerPhone entity);
	}
}
