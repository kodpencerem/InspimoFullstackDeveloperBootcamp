using eCommerce.Application.Services;
using eCommerce.Domain.Repositories;
using eCommerce.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace eCommerce.Infrastructure;
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.TryAddTransient<IProductRepository, Repositories.ElasticSearch.ProductRepository>();
        services.TryAddTransient<IUserRepository, Repositories.ElasticSearch.UserRepository>();
        services.TryAddTransient<ICacheService, RedisCacheService>();
        services.TryAddTransient<IJwtProvider, JwtProvider>();
        return services;
    }
}
