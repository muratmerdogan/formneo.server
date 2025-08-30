using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using vesa.core.DTOs.Lookup;
using vesa.core.Services;

namespace vesa.api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class LookupController : ControllerBase
	{
		private readonly ILookupService _lookupService;

		public LookupController(ILookupService lookupService)
		{
			_lookupService = lookupService;
		}

		[HttpGet("categories")]
		public async Task<IActionResult> GetCategories([FromQuery] string moduleKey)
		{
			var list = await _lookupService.GetCategoriesAsync(moduleKey);
			return Ok(list);
		}


		[HttpGet("modules")]
		public async Task<IActionResult> GetModules()
		{
			var list = await _lookupService.GetModulesAsync();
			return Ok(list);
		}

		[HttpPost("modules")]
		public async Task<IActionResult> CreateModule([FromBody] LookupModuleDto dto)
		{
			var created = await _lookupService.CreateModuleAsync(dto);
			return Ok(created);
		}

		[HttpPut("modules/{id}")]
		public async Task<IActionResult> UpdateModule(Guid id, [FromBody] LookupModuleDto dto)
		{
			var updated = await _lookupService.UpdateModuleAsync(id, dto);
			if (updated == null) return NotFound();
			return Ok(updated);
		}

		[HttpDelete("modules/{id}")]
		public async Task<IActionResult> DeleteModule(Guid id)
		{
			var ok = await _lookupService.DeleteModuleAsync(id);
			if (!ok) return Conflict("Mod l e bal kategori(ler) mevcut veya mod l bulunamad.");
			return NoContent();
		}

		[HttpGet("tree")]
		public async Task<IActionResult> GetTree([FromQuery] string moduleKey)
		{
			var tree = await _lookupService.GetTreeByModuleKeyAsync(moduleKey);
			if (tree == null) return NotFound();
			return Ok(tree);
		}

		[HttpGet("items/{key}")]
		public async Task<IActionResult> GetItemsByKey(string key)
		{
			var list = await _lookupService.GetItemsByKeyAsync(key);
			return Ok(list);
		}

		[HttpPost("categories")]
		public async Task<IActionResult> CreateCategory([FromBody] LookupCategoryDto dto)
		{
			var created = await _lookupService.CreateCategoryAsync(dto);
			return Ok(created);
		}

		[HttpPost("items")]
		public async Task<IActionResult> CreateItem([FromBody] LookupItemDto dto)
		{
			var created = await _lookupService.CreateItemAsync(dto);
			return Ok(created);
		}

		[HttpPut("categories/{id}")]
		public async Task<IActionResult> UpdateCategory(Guid id, [FromBody] LookupCategoryDto dto)
		{
			var updated = await _lookupService.UpdateCategoryAsync(id, dto);
			if (updated == null) return NotFound();
			return Ok(updated);
		}

		[HttpDelete("categories/{id}")]
		public async Task<IActionResult> DeleteCategory(Guid id)
		{
			var ok = await _lookupService.DeleteCategoryAsync(id);
			if (!ok) return Conflict("Kategoriye bal item(ler) mevcut veya kategori bulunamad.");
			return NoContent();
		}

		[HttpPut("items/{id}")]
		public async Task<IActionResult> UpdateItem(Guid id, [FromBody] LookupItemDto dto)
		{
			var updated = await _lookupService.UpdateItemAsync(id, dto);
			if (updated == null) return NotFound();
			return Ok(updated);
		}

		[HttpDelete("items/{id}")]
		public async Task<IActionResult> DeleteItem(Guid id)
		{
			var ok = await _lookupService.DeleteItemAsync(id);
			if (!ok) return NotFound();
			return NoContent();
		}
	}
}


