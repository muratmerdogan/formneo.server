using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using vesa.core.Models.CRM;
using vesa.core.Repositories;

namespace vesa.repository.Repositories
{
	public class CustomerAddressRepository : GenericRepository<CustomerAddress>, ICustomerAddressRepository
	{
		public CustomerAddressRepository(AppDbContext context) : base(context)
		{
		}

		public async Task<List<CustomerAddress>> GetByCustomerIdAsync(Guid customerId)
		{
			var result = await _context.CustomerAddresses
				.Where(x => x.CustomerId == customerId)
				.AsNoTracking()
				.Select(a => new
				{
					Entity = a,
					ConcurrencyToken = EF.Property<uint>(a, "xmin")
				})
				.ToListAsync();

			foreach (var item in result)
			{
				item.Entity.ConcurrencyToken = item.ConcurrencyToken;
			}

			return result.Select(x => x.Entity).ToList();
		}

		public async Task<CustomerAddress> GetDetailAsync(Guid id)
		{
			var result = await _context.CustomerAddresses
				.AsNoTracking()
				.Select(a => new
				{
					Entity = a,
					ConcurrencyToken = EF.Property<uint>(a, "xmin")
				})
				.FirstOrDefaultAsync(x => x.Entity.Id == id);

			if (result == null) return null;

			result.Entity.ConcurrencyToken = result.ConcurrencyToken;
			return result.Entity;
		}
	}
}
