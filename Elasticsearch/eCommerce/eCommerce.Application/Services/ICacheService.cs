namespace eCommerce.Application.Services;
public interface ICacheService
{
    void Set<T>(string name, T data, TimeSpan? expires = null);
    void Remove(string name);
    void Get<T>(string name, out T? data);
}
