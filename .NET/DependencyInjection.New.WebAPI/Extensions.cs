using DependencyInjection.New.WebAPI.Controllers;

namespace DependencyInjection.New.WebAPI;

public static class Extensions
{
    public static IServiceCollection AddMyDependencInjection(this IServiceCollection services)
    {
        //services.AddTransient<ProductService>();
        services.AddTransient<IProductService, NewProductService>();

        return services;
    }
}
