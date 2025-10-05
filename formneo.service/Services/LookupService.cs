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
            var tenantId = _tenantContext?.CurrentTenantId;
            var query = _context.TenantLookupCategories.AsQueryable();
            if (!string.IsNullOrWhiteSpace(moduleKey))
            {
                query = query.Where(x => x.Module != null && x.Module.Key == moduleKey);
            }
            // BaseEntity query filter MainClientId=tenant uygular
            var list = await query.OrderBy(x => x.Key).ToListAsync();
            return _mapper.Map<IEnumerable<LookupCategoryDto>>(list);
		}

		public async Task<IEnumerable<LookupItemDto>> GetItemsByKeyAsync(string key)
		{
            var tenantId = _tenantContext?.CurrentTenantId;
            var items = await _context. TenantLookupItems
                .Where(x => x.IsActive && x.Category.Key == key)
                .OrderBy(x => x.OrderNo)
                .ToListAsync();
            return _mapper.Map<IEnumerable<LookupItemDto>>(items);
		}

		public async Task<LookupCategoryDto> CreateCategoryAsync(LookupCategoryDto dto)
		{
            var entity = _mapper.Map<TenantLookupCategory>(dto);
            _context.TenantLookupCategories.Add(entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<LookupCategoryDto>(entity);
		}

		public async Task<LookupItemDto> CreateItemAsync(LookupItemDto dto)
		{
            var category = await _context.TenantLookupCategories.AsNoTracking().FirstOrDefaultAsync(c => c.Id == dto.CategoryId);
            if (category == null) throw new InvalidOperationException("Kategori bulunamadı.");
            var entity = _mapper.Map<TenantLookupItem>(dto);
            _context. TenantLookupItems.Add(entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<LookupItemDto>(entity);
		}

		public async Task<LookupCategoryDto> UpdateCategoryAsync(System.Guid id, LookupCategoryDto dto)
		{
            var entity = await _context.TenantLookupCategories.FirstOrDefaultAsync(x => x.Id == id);
            if (entity == null) return null;
            _mapper.Map(dto, entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<LookupCategoryDto>(entity);
		}

		public async Task<bool> DeleteCategoryAsync(System.Guid id)
		{
            var hasItems = await _context. TenantLookupItems.AnyAsync(x => x.CategoryId == id);
            if (hasItems) return false;
            var entity = await _context.TenantLookupCategories.FirstOrDefaultAsync(x => x.Id == id);
            if (entity == null) return false;
            _context.TenantLookupCategories.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
		}

		public async Task<LookupItemDto> UpdateItemAsync(System.Guid id, LookupItemDto dto)
		{
            var entity = await _context. TenantLookupItems.FirstOrDefaultAsync(x => x.Id == id);
            if (entity == null) return null;
            _mapper.Map(dto, entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<LookupItemDto>(entity);
		}

		public async Task<bool> DeleteItemAsync(System.Guid id)
		{
            var entity = await _context. TenantLookupItems.FirstOrDefaultAsync(x => x.Id == id);
            if (entity == null) return false;
            _context. TenantLookupItems.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
		}

		// Modules
		public async Task<IEnumerable<LookupModuleDto>> GetModulesAsync()
		{
			var list = await _context.TenantLookupModules.OrderBy(x => x.Key).ToListAsync();
			return _mapper.Map<IEnumerable<LookupModuleDto>>(list);
		}

		public async Task<LookupModuleDto> CreateModuleAsync(LookupModuleDto dto)
		{
			var entity = _mapper.Map<TenantLookupModule>(dto);
			_context.TenantLookupModules.Add(entity);
			await _context.SaveChangesAsync();
			return _mapper.Map<LookupModuleDto>(entity);
		}

		public async Task<LookupModuleDto> UpdateModuleAsync(System.Guid id, LookupModuleDto dto)
		{
			var entity = await _context.TenantLookupModules.FirstOrDefaultAsync(x => x.Id == id);
			if (entity == null) return null;
			_mapper.Map(dto, entity);
			await _context.SaveChangesAsync();
			return _mapper.Map<LookupModuleDto>(entity);
		}

		public async Task<bool> DeleteModuleAsync(System.Guid id)
		{
			var hasCategories = await _context.TenantLookupCategories.AnyAsync(x => x.ModuleId == id);
			if (hasCategories) return false;
			var entity = await _context.TenantLookupModules.FirstOrDefaultAsync(x => x.Id == id);
			if (entity == null) return false;
			_context.TenantLookupModules.Remove(entity);
			await _context.SaveChangesAsync();
			return true;
		}

		// Tree
		public async Task<LookupTreeDto> GetTreeByModuleKeyAsync(string moduleKey)
		{
			var module = await _context.TenantLookupModules.FirstOrDefaultAsync(m => m.Key == moduleKey);
			if (module == null) return null;

            var tenantId = _tenantContext?.CurrentTenantId;
            var categories = await _context.TenantLookupCategories.Where(c => c.ModuleId == module.Id)
                .OrderBy(c => c.Key)
                .ToListAsync();
            var categoryIds = categories.Select(c => c.Id).ToList();
            var items = await _context. TenantLookupItems
                .Where(i => categoryIds.Contains(i.CategoryId))
                .OrderBy(i => i.CategoryId).ThenBy(i => i.OrderNo)
                .ToListAsync();

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
                    IsReadOnly = c.IsReadOnly,
                    Items = _mapper.Map<List<LookupItemDto>>(items.Where(i => i.CategoryId == c.Id).ToList())
                }).ToList()
            };

            return result;
		}


	}
}


