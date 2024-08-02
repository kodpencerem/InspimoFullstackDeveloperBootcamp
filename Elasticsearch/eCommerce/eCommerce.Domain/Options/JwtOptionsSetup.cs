using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace eCommerce.Domain.Options;
public sealed class JwtOptionsSetup
    (IConfiguration configuration) : IConfigureOptions<Jwt>
{
    public void Configure(Jwt options)
    {
        configuration.GetSection("Jwt").Bind(options);
    }
}
