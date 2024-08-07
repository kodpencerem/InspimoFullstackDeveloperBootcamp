using eCommerce.Application.Services;
using Microsoft.Extensions.Caching.Memory;

namespace eCommerce.Infrastructure.Services;
internal sealed class MemoryCacheService(
    IMemoryCache memoryCache) : ICacheService
{
    public void Set<T>(string name, T data, TimeSpan? expires = null)
    {
        memoryCache.Set<T>(name, data, expires ?? TimeSpan.FromMinutes(60));
    }

    public void Get<T>(string name, out T? data)
    {
        memoryCache.TryGetValue(name, out data);
    }

    public void Remove(string name)
    {
        memoryCache.Remove(name);
    }
}
