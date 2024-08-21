using TS.Result;
using TSChat.WebAPI.Models;
using TSChat.WebAPI.Repositories;

namespace TSChat.WebAPI.Services;

public sealed class UserService(
    IUserRepository userRepository)
{
    public async Task<Result<List<User>>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var result = await userRepository.GetAllAsync(cancellationToken);
        return result;
    }
}
