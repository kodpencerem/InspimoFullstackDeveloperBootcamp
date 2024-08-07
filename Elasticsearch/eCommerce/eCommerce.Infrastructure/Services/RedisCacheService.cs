using eCommerce.Application.Services;
using StackExchange.Redis;
using System.Text.Json;

namespace eCommerce.Infrastructure.Services;
internal sealed class RedisCacheService : ICacheService
{
    ConnectionMultiplexer connectionMultiplexer;
    IDatabase db;
    public RedisCacheService()
    {
        connectionMultiplexer = ConnectionMultiplexer.Connect("localhost");
        db = connectionMultiplexer.GetDatabase();

    }
    public void Get<T>(string name, out T? data)
    {
        RedisValue value = db.StringGet(name);

        if (value.HasValue)
        {
            data = JsonSerializer.Deserialize<T>(value!);
        }
        else
        {
            data = default;
        }
    }

    public void Remove(string name)
    {
        db.KeyDelete(name);
    }

    public void Set<T>(string name, T data, TimeSpan? expires = null)
    {
        RedisValue redisValue = new(JsonSerializer.Serialize(data));
        db.StringSet(name, redisValue, expires);
    }
}
