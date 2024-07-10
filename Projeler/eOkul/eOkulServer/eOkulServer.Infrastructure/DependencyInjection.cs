using eOkulServer.Domain.Repositories;
using eOkulServer.Infrastructure.Context;
using eOkulServer.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace eOkulServer.Infrastructure;
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        string connectionString = configuration.GetConnectionString("SqlServer")!;

        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlServer(connectionString);
        });

        services.TryAddScoped<IUserTypeRepository, UserTypeRepository>();
        services.TryAddScoped<IUnitOfWork>(srv => srv.GetRequiredService<ApplicationDbContext>());
        return services;
    }
}
