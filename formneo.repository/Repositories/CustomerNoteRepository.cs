using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using formneo.core.Models.CRM;
using formneo.core.Repositories;

namespace formneo.repository.Repositories
{
	public class CustomerNoteRepository : GenericRepository<CustomerNote>, ICustomerNoteRepository
	{
		public CustomerNoteRepository(AppDbContext context) : base(context)
		{
		}

		public async Task<List<CustomerNote>> GetByCustomerIdAsync(Guid customerId)
		{
			var result = await _context.Set<CustomerNote>()
				.Where(x => x.CustomerId == customerId)
				.OrderByDescending(x => x.Date)
				.AsNoTracking()
				.Select(n => new
				{
					Entity = n,
					ConcurrencyToken = EF.Property<uint>(n, "xmin")
				})
				.ToListAsync();

			foreach (var item in result)
			{
				item.Entity.ConcurrencyToken = item.ConcurrencyToken;
			}

			return result.Select(x => x.Entity).ToList();
		}

		public async Task<CustomerNote> GetDetailAsync(Guid id)
		{
			var result = await _context.Set<CustomerNote>()
				.Include(x => x.Customer)
				.AsNoTracking()
				.Select(n => new
				{
					Entity = n,
					ConcurrencyToken = EF.Property<uint>(n, "xmin")
				})
				.FirstOrDefaultAsync(x => x.Entity.Id == id);

			if (result == null) return null;

			result.Entity.ConcurrencyToken = result.ConcurrencyToken;
			return result.Entity;
		}
	}
}
