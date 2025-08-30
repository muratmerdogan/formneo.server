using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using vesa.core.DTOs;
using vesa.core.Services;
using Microsoft.AspNetCore.Http;
using vesa.core.DTOs.RoleTenants;
using System.Linq;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.AspNetCore.Identity;
using vesa.core.Models;

namespace vesa.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class RoleTenantMenuController : CustomBaseController
    {
        private readonly IRoleTenantMenuService _service;
        private readonly ITenantContext _tenantContext;
        private readonly IUserTenantRoleService _userTenantRoleService;
        private readonly IRoleTenantService _roleTenantService;
        private readonly IMemoryCache _memoryCache;
        private readonly UserManager<UserApp> _userManager;

        public RoleTenantMenuController(IRoleTenantMenuService service, ITenantContext tenantContext, IUserTenantRoleService userTenantRoleService, IRoleTenantService roleTenantService, IMemoryCache memoryCache, UserManager<UserApp> userManager)
        {
            _service = service;
            _tenantContext = tenantContext;
            _userTenantRoleService = userTenantRoleService;
            _roleTenantService = roleTenantService;
            _memoryCache = memoryCache;
            _userManager = userManager;
        }

        [HttpGet("{roleId}/{tenantId}")]
        public async Task<IEnumerable<RoleTenantMenuListDto>> Get(string roleId, Guid tenantId)
        {
            return await _service.GetByRoleAndTenantAsync(roleId, tenantId);
        }

        [HttpPost("bulk-save")]
        public async Task<IActionResult> BulkSave(RoleTenantMenuBulkSaveDto dto)
        {
            await _service.BulkSaveAsync(dto);
            return Ok();
        }

        [HttpDelete("{roleId}/{tenantId}")]
        public async Task<IActionResult> Delete(string roleId, Guid tenantId)
        {
            await _service.RemoveByRoleAndTenantAsync(roleId, tenantId);
            return Ok();
        }

        // Kullanıcı ve tenant'a göre rollerin menülerini döndürür.
        // Kurallar:
        // - tenantId zorunlu, mevcut X-Tenant-Id ile uyuşmalıysa kontrol gerekmez (opsiyonel)
        // - userId zorunlu; kullanıcının tenant içindeki aktif UserTenantRole kayıtları olmalı
        // - Kullanıcı tenant üyesi değilse 403 döner
        [HttpGet("roles-by-user")]
        public async Task<IEnumerable<RoleTenantMenuListDto>> GetRolesMenusByUser([FromQuery] string userId)
        {
            if (string.IsNullOrWhiteSpace(userId))
            {
                throw new vesa.service.Exceptions.ClientSideException("userId zorunludur");
            }

            // Global modda çalışamaz; tenant zorunlu
            var tenantId = _tenantContext?.CurrentTenantId;
            if (tenantId == null)
            {
                throw new vesa.service.Exceptions.ClientSideException("Bu uç nokta global modda kullanılamaz. X-Tenant-Id zorunludur.");
            }

            // Kullanıcının bu tenant'a ait aktif rolü var mı?
            var userRoles = await _userTenantRoleService.GetByUserAndTenantAsync(userId, tenantId.Value);
            if (userRoles == null || !userRoles.Any())
            {
                throw new vesa.service.Exceptions.ClientSideException("Kullanıcı belirtilen tenant'a ait değildir veya aktif rolü yoktur");
            }

            // İlgili rol-tenant için menü izinlerini birleştirerek döndür
            var roleTenantIds = userRoles.Select(utr => utr.RoleTenantId).ToList();

            // RoleTenantId -> RoleId ve TenantId eşlemesi için RoleTenant servisi gerekiyorsa burada genişletilebilir.
            // Varsayım: AspNetRolesTenantMenu doğrudan RoleId + TenantId ile filtrelenir.

            var result = new List<RoleTenantMenuListDto>();
            foreach (var group in userRoles.GroupBy(r => r.RoleTenant))
            {
                var roleId = group.Key.RoleId;
                var menus = await _service.GetByRoleAndTenantAsync(roleId, tenantId.Value);
                result.AddRange(menus);
            }

            return result;
        }

        // Sadece rolleri döndürür (RoleId listesi). Kullanıcı tenant'a ait değilse 403 döner.
        [HttpGet("roles/by-user")]
        public async Task<IEnumerable<string>> GetRolesByUser([FromQuery] string userId)
        {
            if (string.IsNullOrWhiteSpace(userId))
            {
                throw new vesa.service.Exceptions.ClientSideException("userId zorunludur");
            }

            var tenantId = _tenantContext?.CurrentTenantId;
            if (tenantId == null)
            {
                throw new vesa.service.Exceptions.ClientSideException("Bu uç nokta global modda kullanılamaz. X-Tenant-Id zorunludur.");
            }

            var userRoles = await _userTenantRoleService.GetByUserAndTenantAsync(userId, tenantId.Value);
            if (userRoles == null || !userRoles.Any())
            {
                throw new vesa.service.Exceptions.ClientSideException("Kullanıcı belirtilen tenant'a ait değildir veya aktif rolü yoktur");
            }

            var roleIds = userRoles
                .Select(utr => utr.RoleTenant?.RoleId)
                .Where(id => !string.IsNullOrWhiteSpace(id))
                .Distinct()
                .ToList();

            return roleIds;
        }

        // Tenant'taki tüm rol + menüleri döndürür, kullanıcının sahip olduklarını Item.Selected = true olarak işaretler
        // Global mod desteklenmez; CurrentTenantId zorunlu
        [HttpGet("roles-with-menus/by-user")]
        public async Task<RoleTenantWithMenusGetDto> GetTenantRolesWithMenusMarkedForUser([FromQuery] string userId)
        {
            if (string.IsNullOrWhiteSpace(userId))
            {
                throw new vesa.service.Exceptions.ClientSideException("userId zorunludur");
            }

            var tenantId = _tenantContext?.CurrentTenantId;
            if (tenantId == null)
            {
                throw new vesa.service.Exceptions.ClientSideException("Bu uç nokta global modda kullanılamaz. X-Tenant-Id zorunludur.");
            }

            // Tenant'a ait tüm rol+menü yapısını al
            var full = await _roleTenantService.GetByTenantWithMenusAsync(tenantId.Value);

            // Kullanıcının bu tenant'taki aktif rol üyeliklerini al
            var userRoles = await _userTenantRoleService.GetByUserAndTenantAsync(userId, tenantId.Value);
            var assignedRoleIds = new HashSet<string>(userRoles.Select(ur => ur.RoleTenant.RoleId));

            // Kullanıcının sahip olduğu rolleri işaretle
            foreach (var item in full.Items)
            {
                item.Selected = assignedRoleIds.Contains(item.RoleId);
            }

            return full;
        }

        // Yeni: Tenant'taki tüm rolleri kullanıcının sahip olup olmadığını işaretleyerek döner
        [HttpGet("user-role-assignments")]
        public async Task<UserRoleAssignmentGetDto> GetUserRoleAssignments([FromQuery] string userId)
        {
            if (string.IsNullOrWhiteSpace(userId))
            {
                throw new vesa.service.Exceptions.ClientSideException("userId zorunludur");
            }

            var tenantId = _tenantContext?.CurrentTenantId;
            if (tenantId == null)
            {
                throw new vesa.service.Exceptions.ClientSideException("Bu uç nokta global modda kullanılamaz. X-Tenant-Id zorunludur.");
            }

            var result = await _roleTenantService.GetUserRoleAssignmentsAsync(userId, tenantId.Value);
            return result;
        }

        // Yeni: Kullanıcıya rol atama/çıkarma
        [HttpPost("user-role-assignments")]
        public async Task<IActionResult> SaveUserRoleAssignments([FromBody] UserRoleAssignmentSaveDto dto)
        {
            if (dto == null || string.IsNullOrWhiteSpace(dto.UserId))
            {
                return BadRequest("Geçersiz veri");
            }

            var tenantId = _tenantContext?.CurrentTenantId;
            if (tenantId == null)
            {
                return StatusCode(StatusCodes.Status403Forbidden, "Bu uç nokta global modda kullanılamaz. X-Tenant-Id zorunludur.");
            }

            // TenantId DTO içinde yoksa mevcut bağlamdan set et (istenirse)
            if (dto.TenantId == Guid.Empty)
            {
                dto.TenantId = tenantId.Value;
            }

            // Null gelen RoleAssignments listesini boş listeye çevirerek servis içinde null referansı engelle
            dto.RoleAssignments = dto.RoleAssignments ?? new List<UserRoleAssignmentSaveItemDto>();

            await _roleTenantService.SaveUserRoleAssignmentsAsync(dto);

            // Menü cache'ini temizle: key formatı `${username}:{tenantId}:menus`
            var targetUser = await _userManager.FindByIdAsync(dto.UserId);
            var targetUsername = targetUser?.UserName;
            if (!string.IsNullOrWhiteSpace(targetUsername))
            {
                var cacheKey = $"{targetUsername}:{tenantId.Value}:menus";
                _memoryCache.Remove(cacheKey);
            }
            return Ok();
        }
    }
}


