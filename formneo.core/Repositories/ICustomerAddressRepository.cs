using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using formneo.core.Models.CRM;

namespace formneo.core.Repositories
{
	public interface ICustomerAddressRepository : IGenericRepository<CustomerAddress>
	{
		Task<List<CustomerAddress>> GetByCustomerIdAsync(Guid customerId);
		Task<CustomerAddress> GetDetailAsync(Guid id);
	}
}
