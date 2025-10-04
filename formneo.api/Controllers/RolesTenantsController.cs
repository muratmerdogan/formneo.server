using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using formneo.core.DTOs.RoleTenants;
using formneo.core.Services;
using formneo.core.UnitOfWorks;

namespace formneo.api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class RolesTenantsController : CustomBaseController
	{
		private readonly IRoleTenantService _service;
		private readonly IUnitOfWork _unitOfWork;

		public RolesTenantsController(IRoleTenantService service, IUnitOfWork unitOfWork)
		{
			_service = service;
			_unitOfWork = unitOfWork;
		}

		[HttpGet]
		public async Task<IActionResult> All()
		{
			var result = await _service.GetAllAsync();
			return CreateActionResult(result);
		}

		[HttpGet("by-role/{roleId}")]
		public async Task<IEnumerable<RoleTenantListDto>> GetByRole(string roleId)
		{
			var list = await _service.GetByRoleAsync(roleId);
			return list;
		}

		[HttpGet("by-tenant/{tenantId}")]
		public async Task<IEnumerable<RoleTenantListDto>> GetByTenant(Guid tenantId)
		{
			var list = await _service.GetByTenantAsync(tenantId);
			return list;
		}

		// Tenant bazında roller ve onlara bağlı menü izinlerini döndürür
		[HttpGet("with-menus/by-tenant/{tenantId}")]
		public async Task<RoleTenantWithMenusGetDto> GetWithMenusByTenant(Guid tenantId)
		{
			var result = await _service.GetByTenantWithMenusAsync(tenantId);
			return result;
		}

		[HttpGet("by/{roleId}/{tenantId}")]
		public async Task<RoleTenantListDto> GetByRoleAndTenant(string roleId, Guid tenantId)
		{
			var item = await _service.GetByRoleAndTenantAsync(roleId, tenantId);
			return item;
		}

		[HttpPost]
		public async Task<IActionResult> Create(RoleTenantInsertDto dto)
		{
			_unitOfWork.BeginTransaction();
			try
			{
				// Sadece ilgili tenant için sil & kaydet
				await _service.RemoveByRoleAndTenantAsync(dto.RoleId, dto.TenantId);
				var item = await _service.AddAsync(dto);
				await _unitOfWork.CommitAsync();
				return Ok(item);
			}
			catch
			{
				_unitOfWork.Rollback();
				throw;
			}
		}

		[HttpPost("bulk-save")]
		public async Task<IActionResult> BulkSave(RoleTenantBulkSaveDto dto)
		{
			_unitOfWork.BeginTransaction();
			try
			{
				await _service.BulkSaveAsync(dto);
				await _unitOfWork.CommitAsync();
				return Ok();
			}
			catch
			{
				_unitOfWork.Rollback();
				throw;
			}
		}

		// Tenant bazında: önce tüm RoleTenant ve RoleTenantMenu kayıtlarını siler, sonra gelen roller+menülerle kaydeder
		[HttpPost("bulk-save-with-menus")]
		public async Task<IActionResult> BulkSaveWithMenus(RoleTenantWithMenusBulkSaveDto dto)
		{
			_unitOfWork.BeginTransaction();
			try
			{
				await _service.BulkSaveWithMenusAsync(dto);
				await _unitOfWork.CommitAsync();
				return Ok();
			}
			catch
			{
				_unitOfWork.Rollback();
				throw;
			}
		}

		[HttpPut]
		public async Task<IActionResult> Update(RoleTenantUpdateDto dto)
		{
			_unitOfWork.BeginTransaction();
			try
			{
				// Güncelemeden önce sadece ilgili tenant için var olanı sil & yeniden kaydet mantığı
				var existing = await _service.GetByIdGuidAsync(dto.Id);
				if (existing.Data == null)
				{
					return NotFound();
				}
				await _service.RemoveByRoleAndTenantAsync(existing.Data.RoleId, existing.Data.TenantId);
				var insertDto = new RoleTenantInsertDto
				{
					RoleId = existing.Data.RoleId,
					TenantId = existing.Data.TenantId,
					IsActive = dto.IsActive,
					IsLocked = dto.IsLocked
				};
				var item = await _service.AddAsync(insertDto);
				await _unitOfWork.CommitAsync();
				return Ok(item);
			}
			catch
			{
				_unitOfWork.Rollback();
				throw;
			}
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(Guid id)
		{
			var result = await _service.RemoveAsyncByGuid(id);
			return CreateActionResult(result);
		}
	}
}


