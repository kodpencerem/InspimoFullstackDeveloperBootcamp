using TS.Result;
using TSChat.WebAPI.DTOs;
using TSChat.WebAPI.Models;

namespace TSChat.WebAPI.Repositories;

public interface IChatRepository
{
    Task<Result<List<Chat>>> GetAllAsync(Guid userId, Guid toUserId, CancellationToken cancellationToken = default);
    Task<List<ChatUserDto>> GetAllChatUsers(Guid userId, CancellationToken cancellationToken);
    Task<Result<string>> SendMessageAsync(Chat chat, CancellationToken cancellationToken = default);
    Task<int> UnReadChatMessageCount(Guid userId, Guid toUserId, CancellationToken cancellationToken = default);
}