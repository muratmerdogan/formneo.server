using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using vesa.core.DTOs.Lookup;

namespace vesa.core.Services
{
	public interface ILookupService
	{
		Task<IEnumerable<LookupCategoryDto>> GetCategoriesAsync(string moduleKey = null);
		Task<IEnumerable<LookupItemDto>> GetItemsByKeyAsync(string key);
		Task<LookupCategoryDto> CreateCategoryAsync(LookupCategoryDto dto);
		Task<LookupItemDto> CreateItemAsync(LookupItemDto dto);
		Task<LookupCategoryDto> UpdateCategoryAsync(Guid id, LookupCategoryDto dto);
		Task<bool> DeleteCategoryAsync(Guid id);
		Task<LookupItemDto> UpdateItemAsync(Guid id, LookupItemDto dto);
		Task<bool> DeleteItemAsync(Guid id);

		// Modules
		Task<IEnumerable<LookupModuleDto>> GetModulesAsync();
		Task<LookupModuleDto> CreateModuleAsync(LookupModuleDto dto);
		Task<LookupModuleDto> UpdateModuleAsync(Guid id, LookupModuleDto dto);
		Task<bool> DeleteModuleAsync(Guid id);

		// Tree
		Task<LookupTreeDto> GetTreeByModuleKeyAsync(string moduleKey);
	}
}


