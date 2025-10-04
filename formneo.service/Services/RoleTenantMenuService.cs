using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using formneo.core.DTOs;
using formneo.core.Models;
using formneo.core.Repositories;
using formneo.core.Services;
using formneo.core.UnitOfWorks;
using NLayer.Service.Services;

namespace formneo.service.Services
{
    public class RoleTenantMenuService : GlobalServiceWithDto<AspNetRolesTenantMenu, RoleTenantMenuListDto>, IRoleTenantMenuService
    {
        private readonly IGenericRepository<AspNetRolesTenantMenu> _repo;
        private readonly IGenericRepository<AspNetRolesMenu> _roleMenuRepo;

        public RoleTenantMenuService(
            IGenericRepository<AspNetRolesTenantMenu> repo,
            IGenericRepository<AspNetRolesMenu> roleMenuRepo,
            IUnitOfWork unitOfWork,
            IMapper mapper
        ) : base(repo, unitOfWork, mapper)
        {
            _repo = repo;
            _roleMenuRepo = roleMenuRepo;
        }

        public async Task<IEnumerable<RoleTenantMenuListDto>> GetByRoleAndTenantAsync(string roleId, Guid tenantId)
        {
            var list = await _repo.Where(x => x.RoleId == roleId && x.TenantId == tenantId).ToListAsync();
            return _mapper.Map<IEnumerable<RoleTenantMenuListDto>>(list);
        }

        public async Task RemoveByRoleAndTenantAsync(string roleId, Guid tenantId)
        {
            var list = await _repo.Where(x => x.RoleId == roleId && x.TenantId == tenantId).ToListAsync();
            if (list.Any())
            {
                _repo.RemoveRange(list);
            }
        }

        public async Task BulkSaveAsync(RoleTenantMenuBulkSaveDto dto)
        {
            // Mevcut role-tenant menülerini temizle
            var existing = await _repo.Where(x => x.RoleId == dto.RoleId && x.TenantId == dto.TenantId).ToListAsync();
            if (existing.Any())
            {
                _repo.RemoveRange(existing);
            }

            // İstenen menü setini kaydet
            var toInsert = dto.MenuPermissions.Select(m => new AspNetRolesTenantMenu
            {
                Id = Guid.NewGuid(),
                RoleId = dto.RoleId,
                TenantId = dto.TenantId,
                MenuId = m.MenuId,
                CanView = m.CanView,
                CanAdd = m.CanAdd,
                CanEdit = m.CanEdit,
                CanDelete = m.CanDelete,
                Description = "",
                CreatedDate = DateTime.UtcNow,
                CreatedBy = "system",
                UpdatedBy = ""
            });
            await _repo.AddRangeAsync(toInsert);
        }

        public async Task SyncRoleMenusFromGlobalAsync(string roleId, List<AspNetRolesMenu> globalRoleMenus)
        {
            // Bu role ait tüm tenant menülerini al
            var existingTenantMenus = await _repo.Where(x => x.RoleId == roleId).ToListAsync();
            
            // Her tenant için global menü yetkilerini senkronize et
            var tenants = existingTenantMenus.Select(x => x.TenantId).Distinct().ToList();
            
            foreach (var tenantId in tenants)
            {
                // Bu tenant için mevcut yetkileri temizle
                var tenantMenus = existingTenantMenus.Where(x => x.TenantId == tenantId).ToList();
                if (tenantMenus.Any())
                {
                    _repo.RemoveRange(tenantMenus);
                }
                
                // Global yetkileri bu tenant'a kopyala
                var tenantRoleMenus = globalRoleMenus.Select(globalMenu => new AspNetRolesTenantMenu
                {
                    Id = Guid.NewGuid(),
                    RoleId = roleId,
                    TenantId = tenantId,
                    MenuId = globalMenu.MenuId,
                    CanView = globalMenu.CanView,
                    CanAdd = globalMenu.CanAdd,
                    CanEdit = globalMenu.CanEdit,
                    CanDelete = globalMenu.CanDelete,
                    Description = globalMenu.Description,
                    CreatedDate = DateTime.UtcNow,
                    CreatedBy = "system",
                    UpdatedBy = ""
                }).ToList();
                
                if (tenantRoleMenus.Any())
                {
                    await _repo.AddRangeAsync(tenantRoleMenus);
                }
            }
        }
    }
}


