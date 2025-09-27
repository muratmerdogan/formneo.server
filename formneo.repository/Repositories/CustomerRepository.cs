using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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
			var result = await _context.Customers
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
				.Select(c => new
				{
					Entity = c,
					ConcurrencyToken = EF.Property<uint>(c, "xmin")
				})
				.FirstOrDefaultAsync(x => x.Entity.Id == id);

			if (result == null) return null;

			result.Entity.ConcurrencyToken = result.ConcurrencyToken;
			return result.Entity;
		}

		public async Task<Customer> GetByCodeAsync(string code)
		{
			return await _context.Customers
				.AsNoTracking()
				.FirstOrDefaultAsync(x => x.Code == code);
		}

		public async Task<CustomerEmail> GetCustomerEmailAsync(Guid id)
		{
			var result = await _context.CustomerEmails
				.AsNoTracking()
				.Select(e => new
				{
					Entity = e,
					ConcurrencyToken = EF.Property<uint>(e, "xmin")
				})
				.FirstOrDefaultAsync(x => x.Entity.Id == id);

			if (result == null) return null;

			result.Entity.ConcurrencyToken = result.ConcurrencyToken;
			return result.Entity;
		}

		public async Task<List<CustomerEmail>> GetCustomerEmailsByCustomerAsync(Guid customerId)
		{
			var result = await _context.CustomerEmails
				.Where(x => x.CustomerId == customerId)
				.AsNoTracking()
				.Select(e => new
				{
					Entity = e,
					ConcurrencyToken = EF.Property<uint>(e, "xmin")
				})
				.ToListAsync();

			foreach (var item in result)
			{
				item.Entity.ConcurrencyToken = item.ConcurrencyToken;
			}

			return result.Select(x => x.Entity).ToList();
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
			
			var result = await query
				.AsNoTracking()
				.OrderBy(x => x.Name)
				.Skip(skip)
				.Take(take)
				.Select(c => new
				{
					Entity = c,
					ConcurrencyToken = EF.Property<uint>(c, "xmin")
				})
				.ToListAsync();

			foreach (var item in result)
			{
				item.Entity.ConcurrencyToken = item.ConcurrencyToken;
			}

			return result.Select(x => x.Entity).ToList();
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

			var result = await query
				.AsNoTracking()
				.OrderBy(x => x.Name)
				.Skip(skip)
				.Take(take)
				.Select(c => new
				{
					Entity = c,
					ConcurrencyToken = EF.Property<uint>(c, "xmin")
				})
				.ToListAsync();

			foreach (var item in result)
			{
				item.Entity.ConcurrencyToken = item.ConcurrencyToken;
			}

			return result.Select(x => x.Entity).ToList();
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


