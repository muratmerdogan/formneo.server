using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using formneo.core.Models.Security;
using formneo.core.Services;
using formneo.repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace formneo.service.Services
{
    public class PermissionEvaluator : IPermissionEvaluator
    {
        private readonly AppDbContext _db;
        private readonly IMemoryCache _cache;

        public PermissionEvaluator(AppDbContext db, IMemoryCache cache)
        {
            _db = db;
            _cache = cache;
        }

        public bool Has(string userId, Guid? tenantId, string resourceKey, Actions action)
        {
            var mask = GetEffectiveMask(userId, tenantId, resourceKey);
            return ((mask & (int)action) == (int)action);
        }

        public int GetEffectiveMask(string userId, Guid? tenantId, string resourceKey)
        {
            var all = GetAllEffectiveMasks(userId, tenantId);
            return all.TryGetValue(resourceKey, out var mask) ? mask : (int)Actions.Full;
        }

        public IDictionary<string, int> GetAllEffectiveMasks(string userId, Guid? tenantId)
        {
            var cacheKey = $"perm:{userId}:{tenantId?.ToString() ?? "global"}";
            if (_cache.TryGetValue(cacheKey, out IDictionary<string, int> cached))
                return cached;

            // 1) Resource defaults
            var resources = _db.Resources.AsNoTracking().Where(r => r.IsActive).ToList();
            var effective = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
            foreach (var r in resources)
                effective[r.ResourceKey] = r.DefaultMask;

            // 2) User overrides (rol bağımsız MVP)
            var userPerms = _db.UserPermissions.AsNoTracking()
                .Where(up => up.UserId == userId)
                .ToList();

            // Yeni kural: Kullanıcı bir resource için herhangi bir override'a sahipse,
            // default mask yerine SADECE kullanıcı Allowed/Deny birleşimi geçerli olsun.
            // Bu, "sadece view" gibi durumlarda default'taki diğer bitlerin gelmesini engeller.
            var grouped = userPerms.GroupBy(up => up.ResourceKey, StringComparer.OrdinalIgnoreCase);
            foreach (var grp in grouped)
            {
                int allowedAgg = 0;
                int deniedAgg = 0;
                foreach (var up in grp)
                {
                    allowedAgg |= up.AllowedMask;
                    deniedAgg |= up.DeniedMask;
                }
                effective[grp.Key] = allowedAgg & ~deniedAgg;
            }

            _cache.Set(cacheKey, effective, new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10)
            });

            return effective;
        }
    }
}


