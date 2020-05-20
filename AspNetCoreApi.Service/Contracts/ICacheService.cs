using Microsoft.Extensions.Caching.Distributed;
using System.Threading.Tasks;

namespace AspNetCoreApi.Service.Contracts
{
    public interface ICacheService
    {
        Task<T> GetAsync<T>(string key);
        Task SetAsync<T>(string key, T value, DistributedCacheEntryOptions options = null);
        Task RemoveAsync(string key);
    }
}