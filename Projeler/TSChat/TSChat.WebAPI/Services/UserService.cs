using TS.Result;
using TSChat.WebAPI.Models;
using TSChat.WebAPI.Repositories;

namespace TSChat.WebAPI.Services;

public sealed class UserService(
    IUserRepository userRepository,
    IHttpContextAccessor httpContextAccessor
    )
{
    public async Task<Result<List<User>>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var result = await userRepository.GetAllAsync(cancellationToken);

        string? userIdClaim = httpContextAccessor.HttpContext?.User.Claims.FirstOrDefault(p => p.Type == "UserId")?.Value;

        if (userIdClaim is not null)
        {
            Guid userId = Guid.Parse(userIdClaim);
            result.Data = result.Data!.Where(p => p.Id != userId).ToList();
        }

        return result;
    }

    public async Task<Result<User>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        User? user = await userRepository.GetByIdAsync(id, cancellationToken);
        if (user is null)
        {
            return Result<User>.Failure("User not found");
        }

        return user;
    }
}