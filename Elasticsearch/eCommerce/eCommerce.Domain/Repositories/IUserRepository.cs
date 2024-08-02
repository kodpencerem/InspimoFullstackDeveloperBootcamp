using eCommerce.Domain.Entities;

namespace eCommerce.Domain.Repositories;

public interface IUserRepository
{
    Task<Guid> CreateAsync(User user, CancellationToken cancellationToken = default);
    Task<User?> FindByUserNameAndPasswordAsync(string userName, string password, CancellationToken cancellationToken = default);
    Task<bool> IsUserNameExistsAsync(string userName, CancellationToken cancellationToken = default);
    Task<bool> IsEmailExistsAsync(string email, CancellationToken cancellationToken = default);
}