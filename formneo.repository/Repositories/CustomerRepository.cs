using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using vesa.core.Models.CRM;
using vesa.core.Repositories;

namespace vesa.repository.Repositories
{
	public class CustomerRepository : GenericRepository<Customer>, ICustomerRepository
	{
		public CustomerRepository(AppDbContext context) : base(context)
		{
		}

		public async Task<Customer> GetDetailAsync(Guid id)
		{
			return await _context.Customers
				.Include(x => x.Addresses)
				.Include(x => x.Officials)
				.Include(x => x.SecondaryEmails)
				.Include(x => x.Phones)
				.Include(x => x.Notes)
				.Include(x => x.Tags)
				.Include(x => x.Documents)
				.Include(x => x.Sectors)
				.Include(x => x.CustomFields)
				.AsNoTracking()
				.FirstOrDefaultAsync(x => x.Id == id);
		}

		public async Task<List<Customer>> GetListWithDetailsAsync()
		{
			return await _context.Customers
				.Include(x => x.Addresses)
				.Include(x => x.Officials)
				.Include(x => x.SecondaryEmails)
				.Include(x => x.Phones)
				.Include(x => x.Notes)
				.Include(x => x.Tags)
				.Include(x => x.Documents)
				.Include(x => x.Sectors)
				.Include(x => x.CustomFields)
				.AsNoTracking()
				.ToListAsync();
		}
	}
}


