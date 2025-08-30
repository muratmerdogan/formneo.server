using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using vesa.core.DTOs.RoleTenants;
using vesa.core.DTOs;
using vesa.core.Options;
using Microsoft.Extensions.Options;
using vesa.core.Models;
using vesa.core.Repositories;
using vesa.core.Services;
using vesa.core.UnitOfWorks;
using NLayer.Service.Services;


namespace vesa.service.Services
{
	public class RoleTenantService : ServiceWithDto<RoleTenant, RoleTenantListDto>, IRoleTenantService
	{
		private readonly IRoleTenantRepository _repository;
		private readonly IGenericRepository<RoleTenant> _genericRepository;
		private readonly IGenericRepository<AspNetRolesMenu> _roleMenuRepository;
		private readonly IGenericRepository<AspNetRolesTenantMenu> _roleTenantMenuRepository;
        private readonly IGenericRepository<UserTenantRole> _userTenantRoleRepository;
        private readonly ITenantContext _tenantContext;
        private readonly HashSet<string> _globalOnlyRoleIds;

		public RoleTenantService(IRoleTenantRepository repository, IGenericRepository<RoleTenant> genericRepository, IGenericRepository<AspNetRolesMenu> roleMenuRepository, IGenericRepository<AspNetRolesTenantMenu> roleTenantMenuRepository, IGenericRepository<UserTenantRole> userTenantRoleRepository, IUnitOfWork unitOfWork, IMapper mapper, ITenantContext tenantContext, IOptions<RoleScopeOptions> roleScopeOptions)
			: base(genericRepository, unitOfWork, mapper)
		{
			_repository = repository;
			_genericRepository = genericRepository;
			_roleMenuRepository = roleMenuRepository;
			_roleTenantMenuRepository = roleTenantMenuRepository;
            _userTenantRoleRepository = userTenantRoleRepository;
            _tenantContext = tenantContext;
            _globalOnlyRoleIds = roleScopeOptions?.Value?.GlobalOnlyRoleIds?.ToHashSet() ?? new HashSet<string>();
		}

		public async Task<RoleTenantListDto> AddAsync(RoleTenantInsertDto dto)
		{
            if (_globalOnlyRoleIds.Contains(dto.RoleId))
            {
                throw new vesa.service.Exceptions.ClientSideException("Bu rol globaldir ve tenant'a atanamaz.");
            }
			var entity = _mapper.Map<RoleTenant>(dto);
			await _genericRepository.AddAsync(entity);

			// Role'un menülerini tenant'a kopyala
			var roleMenus = await _roleMenuRepository.Where(x => x.RoleId == dto.RoleId).ToListAsync();
			if (roleMenus.Any())
			{
				// Önce mevcut role-tenant menülerini temizle
				var existing = await _roleTenantMenuRepository.Where(x => x.RoleId == dto.RoleId && x.TenantId == dto.TenantId).ToListAsync();
				if (existing.Any())
				{
					_roleTenantMenuRepository.RemoveRange(existing);
				}

				var tenantMenus = roleMenus.Select(m => new AspNetRolesTenantMenu
				{
					Id = Guid.NewGuid(),
					RoleId = dto.RoleId,
					TenantId = dto.TenantId,
					MenuId = m.MenuId,
					CanView = m.CanView,
					CanAdd = m.CanAdd,
					CanEdit = m.CanEdit,
					CanDelete = m.CanDelete,
					Description = m.Description,
					CreatedDate = DateTime.UtcNow,
					CreatedBy = "system",
					UpdatedBy = ""
				});
				await _roleTenantMenuRepository.AddRangeAsync(tenantMenus);
			}
			return _mapper.Map<RoleTenantListDto>(entity);
		}

		public async Task<RoleTenantListDto> UpdateAsync(RoleTenantUpdateDto dto)
		{
			var exists = await _genericRepository.GetByIdStringGuidAsync(dto.Id);
			exists.IsActive = dto.IsActive;
			exists.IsLocked = dto.IsLocked;
			_genericRepository.Update(exists);
			return _mapper.Map<RoleTenantListDto>(exists);
		}

		public async Task<RoleTenantListDto> GetByRoleAndTenantAsync(string roleId, Guid tenantId)
		{
			var entity = await _repository.GetByRoleAndTenantAsync(roleId, tenantId);
			return _mapper.Map<RoleTenantListDto>(entity);
		}

		public async Task<IEnumerable<RoleTenantListDto>> GetByTenantAsync(Guid tenantId)
		{
			var list = await _genericRepository.Where(x => x.TenantId == tenantId).ToListAsync();
			return _mapper.Map<IEnumerable<RoleTenantListDto>>(list);
		}

		public async Task<IEnumerable<RoleTenantListDto>> GetByRoleAsync(string roleId)
		{
			var list = await _genericRepository.Where(x => x.RoleId == roleId).ToListAsync();
			return _mapper.Map<IEnumerable<RoleTenantListDto>>(list);
		}

		public async Task RemoveAllByRoleAsync(string roleId)
		{
			var list = await _genericRepository.Where(x => x.RoleId == roleId).ToListAsync();
			var roleTenantIds = list.Select(x => x.Id).ToList();
			if (roleTenantIds.Any())
			{
				var dependentUserTenantRoles = await _userTenantRoleRepository.Where(x => roleTenantIds.Contains(x.RoleTenantId)).ToListAsync();
				if (dependentUserTenantRoles.Any())
				{
					_userTenantRoleRepository.RemoveRange(dependentUserTenantRoles);
				}
			}
			if (list.Any())
			{
				_genericRepository.RemoveRange(list);
			}

			var tenantMenus = await _roleTenantMenuRepository.Where(x => x.RoleId == roleId).ToListAsync();
			if (tenantMenus.Any())
			{
				_roleTenantMenuRepository.RemoveRange(tenantMenus);
			}
		}

		public async Task RemoveAllByTenantAsync(Guid tenantId)
		{
			var list = await _genericRepository.Where(x => x.TenantId == tenantId).ToListAsync();
			var roleTenantIds = list.Select(x => x.Id).ToList();
			if (roleTenantIds.Any())
			{
				var dependentUserTenantRoles = await _userTenantRoleRepository.Where(x => roleTenantIds.Contains(x.RoleTenantId)).ToListAsync();
				if (dependentUserTenantRoles.Any())
				{
					_userTenantRoleRepository.RemoveRange(dependentUserTenantRoles);
				}
			}
			if (list.Any())
			{
				_genericRepository.RemoveRange(list);
			}

			var tenantMenus = await _roleTenantMenuRepository.Where(x => x.TenantId == tenantId).ToListAsync();
			if (tenantMenus.Any())
			{
				_roleTenantMenuRepository.RemoveRange(tenantMenus);
			}
		}

		public async Task RemoveByRoleAndTenantAsync(string roleId, Guid tenantId)
		{
			var list = await _genericRepository.Where(x => x.RoleId == roleId && x.TenantId == tenantId).ToListAsync();
			var roleTenantIds = list.Select(x => x.Id).ToList();
			if (roleTenantIds.Any())
			{
				var dependentUserTenantRoles = await _userTenantRoleRepository.Where(x => roleTenantIds.Contains(x.RoleTenantId)).ToListAsync();
				if (dependentUserTenantRoles.Any())
				{
					_userTenantRoleRepository.RemoveRange(dependentUserTenantRoles);
				}
			}
			if (list.Any())
			{
				_genericRepository.RemoveRange(list);
			}

			var tenantMenus = await _roleTenantMenuRepository.Where(x => x.RoleId == roleId && x.TenantId == tenantId).ToListAsync();
			if (tenantMenus.Any())
			{
				_roleTenantMenuRepository.RemoveRange(tenantMenus);
			}
		}

		public async Task BulkSaveAsync(RoleTenantBulkSaveDto dto)
		{
			// Role için gelen tenant setini tamamen tazele
			// 1) Role'a ait tüm kayıtları sil
			var allForRole = await _genericRepository.Where(x => x.RoleId == dto.RoleId).ToListAsync();
			if (allForRole.Any())
			{
				_genericRepository.RemoveRange(allForRole);
			}
			// 2) Gelen listeden yeniden ekle
			var entities = dto.Items.Select(i => new RoleTenant
			{
				Id = Guid.NewGuid(),
				RoleId = dto.RoleId,
				TenantId = i.TenantId,
				IsActive = i.IsActive,
				IsLocked = i.IsLocked,
				CreatedDate = DateTime.UtcNow
			});
			await _genericRepository.AddRangeAsync(entities);
		}

		public async Task BulkSaveWithMenusAsync(RoleTenantWithMenusBulkSaveDto dto)
		{
			// Diff tabanlı güncelleme: mevcutları koru, ekle/sil/güncelle uygula
			var existingRoleTenants = await _genericRepository.Where(x => x.TenantId == dto.TenantId).ToListAsync();
			var existingByRoleId = existingRoleTenants.ToDictionary(x => x.RoleId, x => x);

			var incomingItems = (dto.Items ?? new List<RoleWithMenusItemDto>())
				.Where(i => i.Selected && !_globalOnlyRoleIds.Contains(i.RoleId))
				.ToList();
			var incomingByRoleId = incomingItems.ToDictionary(x => x.RoleId, x => x);

			var existingRoleIds = existingByRoleId.Keys.ToHashSet();
			var incomingRoleIds = incomingByRoleId.Keys.ToHashSet();

			var roleIdsToKeep = existingRoleIds.Intersect(incomingRoleIds).ToList();
			var roleIdsToAdd = incomingRoleIds.Except(existingRoleIds).ToList();
			var roleIdsToRemove = existingRoleIds.Except(incomingRoleIds).ToList();

			// 1) Keep: bayrakları güncelle, menüleri senkronize et
			foreach (var roleId in roleIdsToKeep)
			{
				var existing = existingByRoleId[roleId];
				var incoming = incomingByRoleId[roleId];
				existing.IsActive = incoming.IsActive;
				existing.IsLocked = incoming.IsLocked;
				_genericRepository.Update(existing);

				var currentMenus = await _roleTenantMenuRepository.Where(x => x.TenantId == dto.TenantId && x.RoleId == roleId).ToListAsync();
				if (currentMenus.Any())
				{
					_roleTenantMenuRepository.RemoveRange(currentMenus);
				}
				if (incoming.MenuPermissions != null && incoming.MenuPermissions.Any())
				{
					var newMenus = incoming.MenuPermissions.Select(m => new AspNetRolesTenantMenu
					{
						Id = Guid.NewGuid(),
						RoleId = roleId,
						TenantId = dto.TenantId,
						MenuId = m.MenuId,
						CanView = m.CanView,
						CanAdd = m.CanAdd,
						CanEdit = m.CanEdit,
						CanDelete = m.CanDelete,
						Description = string.Empty,
						CreatedDate = DateTime.UtcNow,
						CreatedBy = "system",
						UpdatedBy = string.Empty
					});
					await _roleTenantMenuRepository.AddRangeAsync(newMenus);
				}
			}

			// 2) Add: yeni RoleTenant ve menüler
			var toAddRoleTenants = new List<RoleTenant>();
			var toAddTenantMenus = new List<AspNetRolesTenantMenu>();
			foreach (var roleId in roleIdsToAdd)
			{
				var incoming = incomingByRoleId[roleId];
				toAddRoleTenants.Add(new RoleTenant
				{
					Id = Guid.NewGuid(),
					RoleId = roleId,
					TenantId = dto.TenantId,
					IsActive = incoming.IsActive,
					IsLocked = incoming.IsLocked,
					CreatedDate = DateTime.UtcNow
				});
				if (incoming.MenuPermissions != null && incoming.MenuPermissions.Any())
				{
					toAddTenantMenus.AddRange(incoming.MenuPermissions.Select(m => new AspNetRolesTenantMenu
					{
						Id = Guid.NewGuid(),
						RoleId = roleId,
						TenantId = dto.TenantId,
						MenuId = m.MenuId,
						CanView = m.CanView,
						CanAdd = m.CanAdd,
						CanEdit = m.CanEdit,
						CanDelete = m.CanDelete,
						Description = string.Empty,
						CreatedDate = DateTime.UtcNow,
						CreatedBy = "system",
						UpdatedBy = string.Empty
					}));
				}
			}
			if (toAddRoleTenants.Any())
			{
				await _genericRepository.AddRangeAsync(toAddRoleTenants);
			}
			if (toAddTenantMenus.Any())
			{
				await _roleTenantMenuRepository.AddRangeAsync(toAddTenantMenus);
			}

			// 3) Remove: kullanıcı atamalarını ve menüleri temizle, sonra RoleTenant sil
			if (roleIdsToRemove.Any())
			{
				var roleTenantsToRemove = existingRoleTenants.Where(x => roleIdsToRemove.Contains(x.RoleId)).ToList();
				var roleTenantIdsToRemove = roleTenantsToRemove.Select(x => x.Id).ToList();
				if (roleTenantIdsToRemove.Any())
				{
					var dependentUserTenantRoles = await _userTenantRoleRepository.Where(x => roleTenantIdsToRemove.Contains(x.RoleTenantId)).ToListAsync();
					if (dependentUserTenantRoles.Any())
					{
						_userTenantRoleRepository.RemoveRange(dependentUserTenantRoles);
					}
				}
				var menusToRemove = await _roleTenantMenuRepository.Where(x => x.TenantId == dto.TenantId && roleIdsToRemove.Contains(x.RoleId)).ToListAsync();
				if (menusToRemove.Any())
				{
					_roleTenantMenuRepository.RemoveRange(menusToRemove);
				}
				if (roleTenantsToRemove.Any())
				{
					_genericRepository.RemoveRange(roleTenantsToRemove);
				}
			}
		}

		public async Task<RoleTenantWithMenusGetDto> GetByTenantWithMenusAsync(Guid tenantId)
		{
			// Include ile Role adına erişebilmek için repository üzerinden çek
			var roleTenants = await _repository.GetByTenantWithIncludesAsync(tenantId);
			var result = new RoleTenantWithMenusGetDto
			{
				TenantId = tenantId,
				Items = new List<RoleWithMenusItemDto>()
			};

			if (!roleTenants.Any())
			{
				return result;
			}

			var roleIds = roleTenants.Select(rt => rt.RoleId).Distinct().ToList();
			var tenantMenus = await _roleTenantMenuRepository.Where(x => x.TenantId == tenantId && roleIds.Contains(x.RoleId)).ToListAsync();

			foreach (var rt in roleTenants)
			{
				var item = new RoleWithMenusItemDto
				{
					RoleId = rt.RoleId,
					RoleName = rt.Role?.Name ?? string.Empty,
					IsActive = rt.IsActive,
					IsLocked = rt.IsLocked,
					Selected = true,
					MenuPermissions = new List<MenuPermissionDto>()
				};

				var menusForRole = tenantMenus.Where(m => m.RoleId == rt.RoleId).ToList();
				foreach (var menu in menusForRole)
				{
					item.MenuPermissions.Add(new MenuPermissionDto
					{
						MenuId = menu.MenuId,
						CanView = menu.CanView,
						CanAdd = menu.CanAdd,
						CanEdit = menu.CanEdit,
						CanDelete = menu.CanDelete
					});
				}

				result.Items.Add(item);
			}

			return result;
		}

		// Yeni: Kullanıcı rol atama işlemleri
		public async Task<UserRoleAssignmentGetDto> GetUserRoleAssignmentsAsync(string userId, Guid tenantId)
		{
			// Tenant'taki tüm rolleri (Role dahil) al
			var tenantRoles = (await _repository.GetByTenantWithIncludesAsync(tenantId))
				.Where(x => x.IsActive)
				.ToList();

			// Kullanıcının bu tenant'taki mevcut rol atamalarını al
			var userTenantRoleRepo = _userTenantRoleRepository;
			var userRoles = await userTenantRoleRepo.Where(x =>
				x.UserId == userId &&
				x.RoleTenant.TenantId == tenantId &&
				x.IsActive &&
				x.RoleTenant.IsActive)
				.Include(x => x.RoleTenant)
				.ToListAsync();

			var assignedRoleIds = userRoles.Select(ur => ur.RoleTenant.RoleId).ToHashSet();

			var result = new UserRoleAssignmentGetDto
			{
				UserId = userId,
				TenantId = tenantId,
				Roles = new List<UserRoleAssignmentItemDto>()
			};

			foreach (var rt in tenantRoles)
			{
				var isAssigned = assignedRoleIds.Contains(rt.RoleId);
				var userTenantRole = userRoles.FirstOrDefault(ur => ur.RoleTenant.RoleId == rt.RoleId);

				result.Roles.Add(new UserRoleAssignmentItemDto
				{
					RoleId = rt.RoleId,
					RoleName = rt.Role?.Name ?? string.Empty,
					IsActive = rt.IsActive,
					IsLocked = rt.IsLocked,
					IsAssignedToUser = isAssigned,
					UserTenantRoleId = userTenantRole?.Id
				});
			}

			return result;
		}

		public async Task SaveUserRoleAssignmentsAsync(UserRoleAssignmentSaveDto dto)
		{
			var userTenantRoleRepo = _userTenantRoleRepository;
			var roleTenantRepo = _genericRepository;
			var currentTenantId = _tenantContext.CurrentTenantId ?? Guid.Empty;
			if (currentTenantId == Guid.Empty)
			{
				throw new vesa.service.Exceptions.ClientSideException("Geçerli oturum tenant'ı bulunamadı.");
			}

			// Hedef rol listesi (ShouldAssign = true)
			var desiredRoleIds = (dto.RoleAssignments ?? new List<UserRoleAssignmentSaveItemDto>())
				.Where(x => x.ShouldAssign)
				.Select(x => x.RoleId)
				.Where(x => !string.IsNullOrWhiteSpace(x))
				.ToHashSet();

			_unitOfWork.BeginTransaction();
			try
			{
				// 1) Mevcut tüm kullanıcı-tenant rol atamalarını sil
				var existingUserRoles = await userTenantRoleRepo.Where(x =>
					x.UserId == dto.UserId &&
					x.RoleTenant.TenantId == currentTenantId)
					.Include(x => x.RoleTenant)
					.ToListAsync();
				if (existingUserRoles.Any())
				{
					userTenantRoleRepo.RemoveRange(existingUserRoles);
				}

				// 2) İstenen roller için aktif RoleTenant kayıtlarını toplayıp yeniden ekle
				if (desiredRoleIds.Any())
				{
					var roleTenants = await roleTenantRepo.Where(x =>
						x.TenantId == currentTenantId &&
						x.IsActive &&
						desiredRoleIds.Contains(x.RoleId))
						.ToListAsync();

					var toAdd = roleTenants.Select(rt => new UserTenantRole
					{
						Id = Guid.NewGuid(),
						UserId = dto.UserId,
						RoleTenantId = rt.Id,
						IsActive = true,
						CreatedDate = DateTime.UtcNow,
						CreatedBy = "system"
					}).ToList();

					if (toAdd.Any())
					{
						await userTenantRoleRepo.AddRangeAsync(toAdd);
					}
				}

				await _unitOfWork.CommitAsync();
			}
			catch
			{
				_unitOfWork.Rollback();
				throw;
			}
		}
	}
}


