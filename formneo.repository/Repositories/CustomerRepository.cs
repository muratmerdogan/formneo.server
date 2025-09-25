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

		// Sadece temel bilgileri getiren optimize edilmiş metod
		public async Task<List<Customer>> GetListBasicAsync(int skip = 0, int take = 50, string search = "")
		{
			var query = _context.Customers
				.Include(x => x.CustomerTypeItem)
				.Include(x => x.CategoryItem)
				.AsQueryable();
			
			// Arama filtresi - sadece Name alanında (büyük-küçük harf duyarsız)
			if (!string.IsNullOrEmpty(search))
			{
				query = query.Where(x => x.Name.ToLower().Contains(search.ToLower()));
			}
			
			return await query
				.AsNoTracking()
				.OrderBy(x => x.Name)
				.Skip(skip)
				.Take(take)
				.ToListAsync();
		}

		// Seçici detay yükleme - sadece gerekli ilişkileri yükler
		public async Task<List<Customer>> GetListWithSelectedDetailsAsync(int skip = 0, int take = 50, bool includeAddresses = false, bool includeOfficials = false, bool includeEmails = false, bool includePhones = false, string search = "")
		{
			var query = _context.Customers
				.Include(x => x.CustomerTypeItem)
				.Include(x => x.CategoryItem)
				.AsQueryable();
			
			// Arama filtresi - sadece Name alanında (büyük-küçük harf duyarsız)
			if (!string.IsNullOrEmpty(search))
			{
				query = query.Where(x => x.Name.ToLower().Contains(search.ToLower()));
			}

			if (includeAddresses)
				query = query.Include(x => x.Addresses);
			if (includeOfficials)
				query = query.Include(x => x.Officials);
			if (includeEmails)
				query = query.Include(x => x.SecondaryEmails);
			if (includePhones)
				query = query.Include(x => x.Phones);

			return await query
				.AsNoTracking()
				.OrderBy(x => x.Name)
				.Skip(skip)
				.Take(take)
				.ToListAsync();
		}

		// Toplam kayıt sayısını getir
		public async Task<int> GetTotalCountAsync(string search = "")
		{
			var query = _context.Customers.AsQueryable();
			
			// Arama filtresi - sadece Name alanında (büyük-küçük harf duyarsız)
			if (!string.IsNullOrEmpty(search))
			{
				query = query.Where(x => x.Name.ToLower().Contains(search.ToLower()));
			}
			
			return await query.CountAsync();
		}
	}
}


