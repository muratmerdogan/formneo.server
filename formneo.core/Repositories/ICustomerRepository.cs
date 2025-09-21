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
		
		// Optimize edilmiş metodlar
		Task<List<Customer>> GetListBasicAsync(int skip = 0, int take = 50);
		Task<List<Customer>> GetListWithSelectedDetailsAsync(int skip = 0, int take = 50, bool includeAddresses = false, bool includeOfficials = false, bool includeEmails = false, bool includePhones = false);
		Task<int> GetTotalCountAsync();
	}
}


