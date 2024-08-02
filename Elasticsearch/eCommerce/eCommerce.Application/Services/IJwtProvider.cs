using eCommerce.Domain.Entities;

namespace eCommerce.Application.Services;
public interface IJwtProvider
{
    string CreateToken(User user);
}
