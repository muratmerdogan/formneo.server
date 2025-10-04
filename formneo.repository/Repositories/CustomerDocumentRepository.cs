using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using formneo.core.Models.CRM;
using formneo.core.Repositories;

namespace formneo.repository.Repositories
{
	public class CustomerDocumentRepository : GenericRepository<CustomerDocument>, ICustomerDocumentRepository
	{
		public CustomerDocumentRepository(AppDbContext context) : base(context)
		{
		}

		public async Task<List<CustomerDocument>> GetByCustomerIdAsync(Guid customerId)
		{
			return await _context.Set<CustomerDocument>()
				.Where(x => x.CustomerId == customerId)
				.OrderByDescending(x => x.CreatedDate)
				.AsNoTracking()
				.ToListAsync();
		}

		public async Task<CustomerDocument> GetDetailAsync(Guid id)
		{
			return await _context.Set<CustomerDocument>()
				.Include(x => x.Customer)
				.AsNoTracking()
				.FirstOrDefaultAsync(x => x.Id == id);
		}

		public async Task<List<CustomerDocument>> GetByCategoryAsync(string category)
		{
			return await _context.Set<CustomerDocument>()
				.Where(x => x.Category == category)
				.OrderByDescending(x => x.CreatedDate)
				.AsNoTracking()
				.ToListAsync();
		}

		public async Task<List<CustomerDocument>> GetByCustomerAndCategoryAsync(Guid customerId, string category)
		{
			return await _context.Set<CustomerDocument>()
				.Where(x => x.CustomerId == customerId && x.Category == category)
				.OrderByDescending(x => x.CreatedDate)
				.AsNoTracking()
				.ToListAsync();
		}
	}
}
