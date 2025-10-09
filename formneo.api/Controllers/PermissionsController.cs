using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using formneo.core.DTOs.Security;
using Microsoft.Extensions.Caching.Memory;
using formneo.core.Models.Security;
using formneo.core.Services;
using formneo.repository;
using formneo.core.DTOs.Security;

namespace formneo.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PermissionsController : ControllerBase
    {
        private readonly AppDbContext _db;
        private readonly ITenantContext _tenantContext;
        private readonly IMemoryCache _cache;
        public PermissionsController(AppDbContext db, ITenantContext tenantContext, IMemoryCache cache)
        {
            _db = db;
            _tenantContext = tenantContext;
            _cache = cache;
        }

        private void InvalidatePermissionCacheForUsers(IEnumerable<string> userIds)
        {
            if (userIds == null) return;
            var tenantKey = _tenantContext?.CurrentTenantId?.ToString() ?? "global";
            foreach (var uid in userIds.Where(x => !string.IsNullOrWhiteSpace(x)).Distinct())
            {
                var key = $"perm:{uid}:{tenantKey}";
                _cache.Remove(key);
            }
        }

        private void InvalidateAllUsersPermissionCache()
        {
            try
            {
                var allUserIds = _db.Users.AsNoTracking().Select(u => u.Id).ToList();
                InvalidatePermissionCacheForUsers(allUserIds);
            }
            catch { }
        }

        // Ekranın genel yetkisi: Resource.DefaultMask upsert
        [HttpPost("resource")] 
        public async Task<IActionResult> UpsertResource([FromBody] UpdateResourcePermissionDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.ResourceKey)) return BadRequest("resourceKey is required");

            int mask = dto.Mask ?? (dto.Actions == null ? (int)Actions.Full : dto.Actions.Aggregate(0, (acc, a) => acc | (int)a));

            var res = await _db.Resources.FirstOrDefaultAsync(r => r.ResourceKey == dto.ResourceKey);
            if (res == null)
            {
                res = new Resource
                {
                    ResourceKey = dto.ResourceKey,
                    DefaultMask = mask,
                    IsActive = true
                };
                _db.Resources.Add(res);
            }
            else
            {
                res.DefaultMask = mask;
            }
            await _db.SaveChangesAsync();
            // Default değişti: tüm kullanıcıların efektif mask'ı etkilenebilir
            InvalidateAllUsersPermissionCache();
            return Ok(new { dto.ResourceKey, DefaultMask = mask });
        }

        // Kullanıcı bazında yetkiler: batch upsert
        [HttpPost("users")] 
        public async Task<IActionResult> UpsertUserPermissions([FromBody] UpsertUserPermissionsDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.ResourceKey)) return BadRequest("resourceKey is required");
            if (dto.Items == null || dto.Items.Count == 0) return BadRequest("items required");

            var userIds = dto.Items.Select(i => i.UserId).Where(x => !string.IsNullOrWhiteSpace(x)).Distinct().ToList();
            var existing = await _db.UserPermissions
                .Where(up => up.ResourceKey == dto.ResourceKey && userIds.Contains(up.UserId))
                .ToListAsync();

            foreach (var item in dto.Items)
            {
                var up = existing.FirstOrDefault(x => x.UserId == item.UserId);
                if (up == null)
                {
                    up = new UserPermission
                    {
                        ResourceKey = dto.ResourceKey,
                        UserId = item.UserId,
                        AllowedMask = item.AllowedMask,
                        DeniedMask = item.DeniedMask
                    };
                    _db.UserPermissions.Add(up);
                }
                else
                {
                    up.AllowedMask = item.AllowedMask;
                    up.DeniedMask = item.DeniedMask;
                }
            }

            await _db.SaveChangesAsync();
            // Sadece gönderilen kullanıcıların cache'lerini temizle
            InvalidatePermissionCacheForUsers(userIds);
            return Ok(new { dto.ResourceKey, Count = dto.Items.Count });
        }

        // Ekran bazında: default + kullanıcı kayıtları + effective (tenant filtreli)
        [HttpGet("by-resource/{resourceKey}")]
        public async Task<IActionResult> GetByResource(string resourceKey)
        {
            if (string.IsNullOrWhiteSpace(resourceKey)) return BadRequest();

            var res = await _db.Resources.AsNoTracking().FirstOrDefaultAsync(r => r.ResourceKey == resourceKey);
            var defaultMask = res?.DefaultMask ?? (int)Actions.Full;

            var userPerms = await _db.UserPermissions
                .AsNoTracking()
                .Where(up => up.ResourceKey == resourceKey)
                .Select(up => new { up.UserId, up.AllowedMask, up.DeniedMask })
                .ToListAsync();

            // Kullanıcı adlarını eşle
            var userIds = userPerms.Select(x => x.UserId).Distinct().ToList();
            var userInfos = await _db.Users.AsNoTracking()
                .Where(u => userIds.Contains(u.Id))
                .Select(u => new { u.Id, u.UserName, u.Email, u.FirstName, u.LastName })
                .ToListAsync();
            var userNameMap = userInfos.ToDictionary(x => x.Id, x => x.UserName ?? x.Email);
            string GetFullName(string id)
            {
                var info = userInfos.FirstOrDefault(i => i.Id == id);
                if (info == null) return null;
                var fn = (info.FirstName ?? "").Trim();
                var ln = (info.LastName ?? "").Trim();
                var full = ($"{fn} {ln}").Trim();
                return string.IsNullOrWhiteSpace(full) ? (info.UserName ?? info.Email) : full;
            }

            // Effective yalnızca default + user override (rol yok)
            var effective = new Dictionary<string, int>();
            foreach (var g in userPerms.GroupBy(x => x.UserId))
            {
                var cur = defaultMask;
                foreach (var up in g)
                {
                    cur = (cur | up.AllowedMask) & ~up.DeniedMask;
                }
                effective[g.Key] = cur;
            }

            var usersWithNames = userPerms.Select(x => new
            {
                x.UserId,
                UserName = userNameMap.TryGetValue(x.UserId, out var name) ? name : null,
                FullName = GetFullName(x.UserId),
                x.AllowedMask,
                x.DeniedMask
            });

            return Ok(new
            {
                ResourceKey = resourceKey,
                DefaultMask = defaultMask,
                Users = usersWithNames,
                EffectiveByUser = effective
            });
        }

        // Kullanıcı bazında: tüm ekranlar + effective mask (tenant filtreli)
        [HttpGet("by-user/{userId}")]
        public async Task<IActionResult> GetByUser(string userId)
        {
            if (string.IsNullOrWhiteSpace(userId)) return BadRequest();

            var resources = await _db.Resources.AsNoTracking().Where(r => r.IsActive).ToListAsync();
            var defaultDict = resources.ToDictionary(r => r.ResourceKey, r => r.DefaultMask);

            var userPerms = await _db.UserPermissions
                .AsNoTracking()
                .Where(up => up.UserId == userId)
                .ToListAsync();

            var effective = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
            foreach (var res in resources)
            {
                effective[res.ResourceKey] = res.DefaultMask;
            }

            foreach (var up in userPerms)
            {
                var cur = effective.TryGetValue(up.ResourceKey, out var v) ? v : (int)Actions.Full;
                cur = (cur | up.AllowedMask) & ~up.DeniedMask;
                effective[up.ResourceKey] = cur;
            }

            return Ok(new
            {
                UserId = userId,
                Defaults = defaultDict,
                UserOverrides = userPerms.Select(p => new { p.ResourceKey, p.AllowedMask, p.DeniedMask }),
                Effective = effective
            });
        }
        // Tek transaction ile hem ekran default’u hem de kullanıcı izinleri
        [HttpPost("upsertAll")]
        public async Task<IActionResult> UpsertAll([FromBody] UpsertAllPermissionsDto dto)
        {
                if (dto?.Resource == null || string.IsNullOrWhiteSpace(dto.Resource.ResourceKey))
                return BadRequest("resource required");

            using var tx = await _db.Database.BeginTransactionAsync();
            try
            {
                // Normalize resourceKey for consistent comparisons
                var resourceKeyNormalized = dto.Resource.ResourceKey?.Trim();
                if (string.IsNullOrWhiteSpace(resourceKeyNormalized)) return BadRequest("resourceKey is required");

                // 1) Resource default upsert
                int resMask = dto.Resource.Mask ?? (dto.Resource.Actions == null ? (int)Actions.Full : dto.Resource.Actions.Aggregate(0, (acc, a) => acc | (int)a));
                var res = await _db.Resources.FirstOrDefaultAsync(r => r.ResourceKey.ToLower() == resourceKeyNormalized.ToLower());
                if (res == null)
                {
                    res = new Resource { ResourceKey = resourceKeyNormalized, DefaultMask = resMask, IsActive = true };
                    _db.Resources.Add(res);
                }
                else
                {
                    res.DefaultMask = resMask;
                }
                await _db.SaveChangesAsync();

                // 2) User permissions authoritative sync for this resource
                var resourceKey = resourceKeyNormalized;
                var existingAll = await _db.UserPermissions
                    .Where(up => up.ResourceKey.ToLower() == resourceKey.ToLower())
                    .ToListAsync();

                // If no users provided (null or empty), remove all existing permissions for this resource
                if (dto.Users == null || dto.Users.Count == 0)
                {
                    if (existingAll.Count > 0)
                    {
                        _db.UserPermissions.RemoveRange(existingAll);
                        await _db.SaveChangesAsync();
                        // Bu resource'a ait tüm kullanıcılar: cache temizle
                        InvalidatePermissionCacheForUsers(existingAll.Select(x => x.UserId));
                    }
                }
                else
                {
                    var payloadUserIds = dto.Users
                        .Select(i => i.UserId)
                        .Where(x => !string.IsNullOrWhiteSpace(x))
                        .Distinct()
                        .ToHashSet();

                    // Upsert for provided users
                    var existingByUser = existingAll.ToDictionary(x => x.UserId, x => x);
                    foreach (var item in dto.Users)
                    {
                        if (string.IsNullOrWhiteSpace(item.UserId)) continue;
                        if (!existingByUser.TryGetValue(item.UserId, out var up))
                        {
                            up = new UserPermission
                            {
                                ResourceKey = resourceKey,
                                UserId = item.UserId,
                                AllowedMask = item.AllowedMask,
                                DeniedMask = item.DeniedMask
                            };
                            _db.UserPermissions.Add(up);
                        }
                        else
                        {
                            up.AllowedMask = item.AllowedMask;
                            up.DeniedMask = item.DeniedMask;
                        }
                    }

                    // Delete users that are not in payload
                    var toDelete = existingAll.Where(x => !payloadUserIds.Contains(x.UserId)).ToList();
                    if (toDelete.Count > 0)
                    {
                        _db.UserPermissions.RemoveRange(toDelete);
                    }

                    await _db.SaveChangesAsync();

                    // Payload'taki ve silinen kullanıcıların cache'lerini temizle
                    var affectedUsers = payloadUserIds.Union(toDelete.Select(x => x.UserId)).ToList();
                    InvalidatePermissionCacheForUsers(affectedUsers);
                }

                await tx.CommitAsync();
                // Resource değiştiyse geniş kapsamlı etkiler olabilir: tüm kullanıcıların cache'ini temizle (güvenli tercih)
                InvalidateAllUsersPermissionCache();
                return Ok(new { dto.Resource.ResourceKey, DefaultMask = resMask, Users = dto.Users?.Count ?? 0 });
            }
            catch
            {
                await tx.RollbackAsync();
                throw;
            }
        }
    }
}


