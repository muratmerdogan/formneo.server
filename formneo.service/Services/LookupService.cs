using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using formneo.core.DTOs.Lookup;
using formneo.core.Models.Lookup;
using formneo.core.Services;
using formneo.repository;

namespace formneo.service.Services
{
	public class LookupService : ILookupService
	{
		private readonly AppDbContext _context;
		private readonly IMapper _mapper;
        private readonly ITenantContext _tenantContext;

		public LookupService(AppDbContext context, IMapper mapper, ITenantContext tenantContext)
		{
			_context = context;
			_mapper = mapper;
            _tenantContext = tenantContext;
		}

		public async Task<IEnumerable<LookupCategoryDto>> GetCategoriesAsync(string moduleKey = null)
		{
			var query = _context.LookupCategories.AsQueryable();
			if (!string.IsNullOrWhiteSpace(moduleKey))
			{
				query = query.Where(x => x.Module != null && x.Module.Key == moduleKey);
			}
			var tenantId = _tenantContext?.CurrentTenantId;
			var list = await query
				.Where(x => x.TenantId == null || (tenantId != null && x.TenantId == tenantId))
				.OrderBy(x => x.Key)
				.ToListAsync();
			if (tenantId != null)
			{
				list = list
					.GroupBy(c => c.Key)
					.Select(g => g.FirstOrDefault(c => c.TenantId == tenantId) ?? g.First())
					.ToList();
			}
			return _mapper.Map<IEnumerable<LookupCategoryDto>>(list);
		}

		public async Task<IEnumerable<LookupItemDto>> GetItemsByKeyAsync(string key)
		{
			var tenantId = _tenantContext?.CurrentTenantId;
			var items = await _context.LookupItems
				.Where(x => x.IsActive && x.Category.Key == key && (x.TenantId == null || (tenantId != null && x.TenantId == tenantId)))
				.OrderBy(x => x.OrderNo)
				.ToListAsync();
			if (tenantId != null)
			{
				items = items
					.GroupBy(i => i.Code)
					.Select(g => g.FirstOrDefault(i => i.TenantId == tenantId) ?? g.First())
					.OrderBy(i => i.OrderNo)
					.ToList();
			}
			return _mapper.Map<IEnumerable<LookupItemDto>>(items);
		}

		public async Task<LookupCategoryDto> CreateCategoryAsync(LookupCategoryDto dto)
		{
			var entity = _mapper.Map<LookupCategory>(dto);
			var tenantId = _tenantContext?.CurrentTenantId;
			if (tenantId != null && dto.IsTenantScoped)
			{
				entity.TenantId = tenantId;
			}
			_context.LookupCategories.Add(entity);
			await _context.SaveChangesAsync();
			return _mapper.Map<LookupCategoryDto>(entity);
		}

		public async Task<LookupItemDto> CreateItemAsync(LookupItemDto dto)
		{
			var entity = _mapper.Map<LookupItem>(dto);
			var tenantId = _tenantContext?.CurrentTenantId;
			var category = await _context.LookupCategories.AsNoTracking().FirstOrDefaultAsync(c => c.Id == dto.CategoryId);
			if (category == null) throw new InvalidOperationException("Kategori bulunamadı.");
			if (tenantId != null)
			{
				if (category.TenantId != null && category.TenantId != tenantId)
					throw new InvalidOperationException("Başka bir tenant'a ait kategoriye item eklenemez.");
				// Kiracı, kendi item'ını oluşturuyorsa overlay olarak TenantId'yı damgala
				entity.TenantId = tenantId;
			}
			_context.LookupItems.Add(entity);
			await _context.SaveChangesAsync();
			return _mapper.Map<LookupItemDto>(entity);
		}

		public async Task<LookupCategoryDto> UpdateCategoryAsync(System.Guid id, LookupCategoryDto dto)
		{
			var entity = await _context.LookupCategories.FirstOrDefaultAsync(x => x.Id == id);
			if (entity == null) return null;
			_mapper.Map(dto, entity);
			await _context.SaveChangesAsync();
			return _mapper.Map<LookupCategoryDto>(entity);
		}

		public async Task<bool> DeleteCategoryAsync(System.Guid id)
		{
			var hasItems = await _context.LookupItems.AnyAsync(x => x.CategoryId == id);
			if (hasItems) return false;
			var entity = await _context.LookupCategories.FirstOrDefaultAsync(x => x.Id == id);
			if (entity == null) return false;
			_context.LookupCategories.Remove(entity);
			await _context.SaveChangesAsync();
			return true;
		}

		public async Task<LookupItemDto> UpdateItemAsync(System.Guid id, LookupItemDto dto)
		{
			var entity = await _context.LookupItems.FirstOrDefaultAsync(x => x.Id == id);
			if (entity == null) return null;
			var tenantId = _tenantContext?.CurrentTenantId;
			// Global item'larda düzenleme engeli (global admin dışı için interceptor zaten engeller)
			if (entity.TenantId == null)
			{
				throw new InvalidOperationException("Global lookup item d\u00fczenlenemez.");
			}
			// Kirac\u0131 yaln\u0131zca kendi item'\u0131n\u0131 d\u00fczenleyebilir
			if (tenantId == null || entity.TenantId != tenantId)
			{
				throw new InvalidOperationException("Ba\u015fka bir tenant'a ait lookup item d\u00fczenlenemez.");
			}
			// Kategori erişim doğrulaması: item başka bir tenant kategorisine taşınmasın
			if (dto.CategoryId != entity.CategoryId && dto.CategoryId != System.Guid.Empty)
			{
				var newCategory = await _context.LookupCategories.AsNoTracking().FirstOrDefaultAsync(c => c.Id == dto.CategoryId);
				if (newCategory == null) throw new InvalidOperationException("Kategori bulunamadı.");
				if (newCategory.TenantId != null && newCategory.TenantId != tenantId)
					throw new InvalidOperationException("Başka bir tenant'a ait kategoriye taşıma yapılamaz.");
			}
			_mapper.Map(dto, entity);
			await _context.SaveChangesAsync();
			return _mapper.Map<LookupItemDto>(entity);
		}

		public async Task<bool> DeleteItemAsync(System.Guid id)
		{
			var entity = await _context.LookupItems.FirstOrDefaultAsync(x => x.Id == id);
			if (entity == null) return false;
			var tenantId = _tenantContext?.CurrentTenantId;
			// Global item'larda silme engeli
			if (entity.TenantId == null)
			{
				throw new InvalidOperationException("Global lookup item silinemez.");
			}
			// Kirac\u0131 yaln\u0131zca kendi item'\u0131n\u0131 silebilir
			if (tenantId == null || entity.TenantId != tenantId)
			{
				throw new InvalidOperationException("Ba\u015fka bir tenant'a ait lookup item silinemez.");
			}
			_context.LookupItems.Remove(entity);
			await _context.SaveChangesAsync();
			return true;
		}

		// Modules
		public async Task<IEnumerable<LookupModuleDto>> GetModulesAsync()
		{
			var list = await _context.LookupModules.OrderBy(x => x.Key).ToListAsync();
			return _mapper.Map<IEnumerable<LookupModuleDto>>(list);
		}

		public async Task<LookupModuleDto> CreateModuleAsync(LookupModuleDto dto)
		{
			var entity = _mapper.Map<LookupModule>(dto);
			_context.LookupModules.Add(entity);
			await _context.SaveChangesAsync();
			return _mapper.Map<LookupModuleDto>(entity);
		}

		public async Task<LookupModuleDto> UpdateModuleAsync(System.Guid id, LookupModuleDto dto)
		{
			var entity = await _context.LookupModules.FirstOrDefaultAsync(x => x.Id == id);
			if (entity == null) return null;
			_mapper.Map(dto, entity);
			await _context.SaveChangesAsync();
			return _mapper.Map<LookupModuleDto>(entity);
		}

		public async Task<bool> DeleteModuleAsync(System.Guid id)
		{
			var hasCategories = await _context.LookupCategories.AnyAsync(x => x.ModuleId == id);
			if (hasCategories) return false;
			var entity = await _context.LookupModules.FirstOrDefaultAsync(x => x.Id == id);
			if (entity == null) return false;
			_context.LookupModules.Remove(entity);
			await _context.SaveChangesAsync();
			return true;
		}

		// Tree
		public async Task<LookupTreeDto> GetTreeByModuleKeyAsync(string moduleKey)
		{
			var module = await _context.LookupModules.FirstOrDefaultAsync(m => m.Key == moduleKey);
			if (module == null) return null;

			var tenantId = _tenantContext?.CurrentTenantId;
			var categories = await _context.LookupCategories
				.Where(c => c.ModuleId == module.Id && (c.TenantId == null || (tenantId != null && c.TenantId == tenantId)))
				.OrderBy(c => c.Key)
				.ToListAsync();
			if (tenantId != null)
			{
				categories = categories
					.GroupBy(c => c.Key)
					.Select(g => g.FirstOrDefault(c => c.TenantId == tenantId) ?? g.First())
					.ToList();
			}

			var categoryIds = categories.Select(c => c.Id).ToList();
			var items = await _context.LookupItems
				.Where(i => categoryIds.Contains(i.CategoryId) && (i.TenantId == null || (tenantId != null && i.TenantId == tenantId)))
				.OrderBy(i => i.CategoryId).ThenBy(i => i.OrderNo)
				.ToListAsync();
			if (tenantId != null)
			{
				items = items
					.GroupBy(i => new { i.CategoryId, i.Code })
					.Select(g => g.FirstOrDefault(i => i.TenantId == tenantId) ?? g.First())
					.OrderBy(i => i.CategoryId).ThenBy(i => i.OrderNo)
					.ToList();
			}

			var result = new LookupTreeDto
			{
				ModuleId = module.Id,
				ModuleKey = module.Key,
				ModuleName = module.Name,
				Categories = categories.Select(c => new LookupCategoryWithItemsDto
				{
					Id = c.Id,
					Key = c.Key,
					Description = c.Description,
					IsTenantScoped = c.IsTenantScoped,
					IsReadOnly = c.IsReadOnly,
					Items = _mapper.Map<List<LookupItemDto>>(items.Where(i => i.CategoryId == c.Id).ToList())
				}).ToList()
			};

			return result;
		}


	}
}


