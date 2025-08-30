namespace vesa.api.Controllers
{
    using Microsoft.Extensions.Caching.Memory;
    using System;

    namespace MyApplication.Services
    {
        public class MyCacheService
        {
            private readonly IMemoryCache _memoryCache;
            private const string CacheKey = "MyCacheKey";

            public MyCacheService(IMemoryCache memoryCache)
            {
                _memoryCache = memoryCache;
            }

            public void SetCache(string value)
            {
                var cacheEntryOptions = new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5), // 5 dakika boyunca cache'de kalacak
                    SlidingExpiration = TimeSpan.FromMinutes(2) // 2 dakika boyunca erişilmezse cache'den çıkarılacak
                };

                _memoryCache.Set(CacheKey, value, cacheEntryOptions);
                Console.WriteLine("Value has been set in cache.");
            }

            public string GetCache()
            {
                if (_memoryCache.TryGetValue(CacheKey, out string value))
                {
                    Console.WriteLine("Cache hit! Value retrieved from cache.");
                    return value;
                }

                Console.WriteLine("Cache miss! Value not found in cache.");
                return null;
            }
        }
    }

}
