using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using formneo.core.Models.CRM;
using formneo.core.Repositories;

namespace formneo.repository.Repositories
{
	public class CustomerPhoneRepository : GenericRepository<CustomerPhone>, ICustomerPhoneRepository
	{
		public CustomerPhoneRepository(AppDbContext context) : base(context)
		{
		}

		public async Task<CustomerPhone> GetDetailAsync(Guid id)
		{
			var result = await _context.CustomerPhones
				.AsNoTracking()
				.Select(p => new
				{
					Entity = p,
					ConcurrencyToken = EF.Property<uint>(p, "xmin")
				})
				.FirstOrDefaultAsync(x => x.Entity.Id == id);

			if (result == null) return null;

			result.Entity.ConcurrencyToken = result.ConcurrencyToken;
			return result.Entity;
		}

		public async Task<List<CustomerPhone>> GetByCustomerIdAsync(Guid customerId)
		{
			var result = await _context.CustomerPhones
				.Where(x => x.CustomerId == customerId)
				.AsNoTracking()
				.Select(p => new
				{
					Entity = p,
					ConcurrencyToken = EF.Property<uint>(p, "xmin")
				})
				.ToListAsync();

			foreach (var item in result)
			{
				item.Entity.ConcurrencyToken = item.ConcurrencyToken;
			}

			return result.Select(x => x.Entity).ToList();
		}

		public uint GetConcurrencyToken(CustomerPhone entity)
		{
			return _context.Entry(entity).Property<uint>("xmin").CurrentValue;
		}
	}
}
