using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using vesa.core.Models.CRM;

namespace vesa.core.Repositories
{
	public interface ICustomerRepository : IGenericRepository<Customer>
	{
		Task<Customer> GetDetailAsync(Guid id);
		Task<List<Customer>> GetListWithDetailsAsync();
	}
}


