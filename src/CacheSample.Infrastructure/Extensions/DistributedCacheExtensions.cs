using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;

namespace CacheSample.Infrastructure.Extensions
{
    public static class DistributedCacheExtensions
    {
        public static bool TryGetValue<TItem>(this IDistributedCache cache, string key, out TItem value)
            where TItem : class
        {
            value = null;
            try
            {
                string cacheData = cache.GetString(key);
                
                if (string.IsNullOrEmpty(cacheData))
                    return false;

                value = JsonSerializer.Deserialize<TItem>(cacheData);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public static void SetValue<TItem>(this IDistributedCache cache, string Key, TItem value, DistributedCacheEntryOptions options = null)
            where TItem : class
        {
            try
            {
                string cacheData = JsonSerializer.Serialize(value);
                cache.SetString(Key, cacheData, options);
            }
            catch
            {

            }
        }
    }
}
