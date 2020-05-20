using AspNetCoreApi.Service.Contracts;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System.Text;
using System.Threading.Tasks;

namespace AspNetCoreApi.Service.Services
{
    public class CacheService : ICacheService
    {
        private readonly IDistributedCache cache;

        public CacheService(IDistributedCache cache)
        {
            this.cache = cache;
        }

        public async Task<T> GetAsync<T>(string key) 
        {
            var bytes = await cache.GetAsync(key);
            if (bytes == null)
                return default;

            var serialize = Encoding.UTF8.GetString(bytes);

            return JsonConvert.DeserializeObject<T>(serialize);
        }

        public async Task SetAsync<T>(string key, T value, DistributedCacheEntryOptions options = null)
        {
            var serialize = JsonConvert.SerializeObject(value, new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });

            var bytes = Encoding.UTF8.GetBytes(serialize);

            if (options != null)
                await cache.SetAsync(key, bytes, options);
            else
                await cache.SetAsync(key, bytes);
        }

        public async Task RemoveAsync(string key)
        {
            await cache.RemoveAsync(key);
        }
    }
}
