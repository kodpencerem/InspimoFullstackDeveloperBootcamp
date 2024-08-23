using TS.Result;
using TSChat.WebAPI.Models;

namespace TSChat.WebAPI.Repositories;

public interface IUserRepository
{
    Task<Result<string>> CreateAsync(User user, CancellationToken cancellationToken = default);
    Task<bool> IsUserNameExist(string userName, CancellationToken cancellationToken = default);
    Task<User?> GetUserByUserNameAndPasswordAsync(string userName, string password, CancellationToken cancellationToken = default);
    Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Result<string>> UpdateAsync(User user, CancellationToken cancellationToken = default);
    Task<Result<List<User>>> GetAllAsync(CancellationToken cancellationToken = default);
}
