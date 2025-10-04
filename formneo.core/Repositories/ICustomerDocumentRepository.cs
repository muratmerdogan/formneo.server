using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using formneo.core.Models.CRM;

namespace formneo.core.Repositories
{
	public interface ICustomerDocumentRepository : IGenericRepository<CustomerDocument>
	{
		Task<List<CustomerDocument>> GetByCustomerIdAsync(Guid customerId);
		Task<CustomerDocument> GetDetailAsync(Guid id);
		Task<List<CustomerDocument>> GetByCategoryAsync(string category);
		Task<List<CustomerDocument>> GetByCustomerAndCategoryAsync(Guid customerId, string category);
	}
}
