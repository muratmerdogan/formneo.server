using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using vesa.core.DTOs.Lookup;
using vesa.core.Models.Lookup;
using vesa.core.Services;
using vesa.repository;

namespace vesa.service.Services
{
	public class LookupService : ILookupService
	{
		private readonly AppDbContext _context;
		private readonly IMapper _mapper;

		public LookupService(AppDbContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}

		public async Task<IEnumerable<LookupCategoryDto>> GetCategoriesAsync(string moduleKey = null)
		{
			var query = _context.LookupCategories.AsQueryable();
			if (!string.IsNullOrWhiteSpace(moduleKey))
			{
				query = query.Where(x => x.Module != null && x.Module.Key == moduleKey);
			}
			var list = await query.OrderBy(x => x.Key).ToListAsync();
			return _mapper.Map<IEnumerable<LookupCategoryDto>>(list);
		}

		public async Task<IEnumerable<LookupItemDto>> GetItemsByKeyAsync(string key)
		{
			var items = await _context.LookupItems
				.Where(x => x.IsActive && x.Category.Key == key)
				.OrderBy(x => x.OrderNo)
				.ToListAsync();
			return _mapper.Map<IEnumerable<LookupItemDto>>(items);
		}

		public async Task<LookupCategoryDto> CreateCategoryAsync(LookupCategoryDto dto)
		{
			var entity = _mapper.Map<LookupCategory>(dto);
			_context.LookupCategories.Add(entity);
			await _context.SaveChangesAsync();
			return _mapper.Map<LookupCategoryDto>(entity);
		}

		public async Task<LookupItemDto> CreateItemAsync(LookupItemDto dto)
		{
			var entity = _mapper.Map<LookupItem>(dto);
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
			_mapper.Map(dto, entity);
			await _context.SaveChangesAsync();
			return _mapper.Map<LookupItemDto>(entity);
		}

		public async Task<bool> DeleteItemAsync(System.Guid id)
		{
			var entity = await _context.LookupItems.FirstOrDefaultAsync(x => x.Id == id);
			if (entity == null) return false;
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

			var categories = await _context.LookupCategories
				.Where(c => c.ModuleId == module.Id)
				.OrderBy(c => c.Key)
				.ToListAsync();

			var categoryIds = categories.Select(c => c.Id).ToList();
			var items = await _context.LookupItems
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
					IsTenantScoped = c.IsTenantScoped,
					IsReadOnly = c.IsReadOnly,
					Items = _mapper.Map<List<LookupItemDto>>(items.Where(i => i.CategoryId == c.Id).ToList())
				}).ToList()
			};

			return result;
		}
	}
}


