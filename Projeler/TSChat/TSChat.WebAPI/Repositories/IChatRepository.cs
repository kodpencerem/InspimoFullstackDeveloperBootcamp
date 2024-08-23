using TS.Result;
using TSChat.WebAPI.Models;

namespace TSChat.WebAPI.Repositories;

public interface IChatRepository
{
    Task<Result<List<Chat>>> GetAllAsync(Guid userId, Guid toUserId, CancellationToken cancellationToken = default);

    Task<Result<string>> SendMessageAsync(Chat chat, CancellationToken cancellationToken = default);
}