using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using vesa.core.Models.CRM;
using vesa.core.Repositories;

namespace vesa.repository.Repositories
{
	public class CustomerNoteRepository : GenericRepository<CustomerNote>, ICustomerNoteRepository
	{
		public CustomerNoteRepository(AppDbContext context) : base(context)
		{
		}

		public async Task<List<CustomerNote>> GetByCustomerIdAsync(Guid customerId)
		{
			return await _context.Set<CustomerNote>()
				.Where(x => x.CustomerId == customerId)
				.OrderByDescending(x => x.Date)
				.AsNoTracking()
				.ToListAsync();
		}

		public async Task<CustomerNote> GetDetailAsync(Guid id)
		{
			return await _context.Set<CustomerNote>()
				.Include(x => x.Customer)
				.AsNoTracking()
				.FirstOrDefaultAsync(x => x.Id == id);
		}
	}
}
