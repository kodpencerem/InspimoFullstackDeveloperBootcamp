using Microsoft.AspNetCore.SignalR;
using TS.Result;
using TSChat.WebAPI.DTOs;
using TSChat.WebAPI.Hubs;
using TSChat.WebAPI.Models;
using TSChat.WebAPI.Repositories;

namespace TSChat.WebAPI.Services;

public sealed class ChatService(
    IChatRepository chatRepository,
    IUserRepository userRepository,
    IHttpContextAccessor httpContextAccessor,
    IHubContext<ChatHub> hubContext
    )
{
    public async Task<Result<List<Chat>>> GetAllAsync(Guid toUserId, CancellationToken cancellationToken = default)
    {
        var userId = httpContextAccessor.HttpContext?.User.Claims.FirstOrDefault(p => p.Type == "UserId")?.Value;

        var result = await chatRepository.GetAllAsync(Guid.Parse(userId!), toUserId, cancellationToken);

        if (result.IsSuccessful)
        {
            result.Data = result.Data!.OrderBy(p => p.SendDate).ToList();
        }

        return result;
    }

    public async Task<Result<string>> SendMessageAsync(SendMessageDto request, CancellationToken cancellationToken = default)
    {
        var userId = httpContextAccessor.HttpContext?.User.Claims.FirstOrDefault(p => p.Type == "UserId")?.Value;

        Chat chat = new()
        {
            Message = request.Message,
            ToUserId = request.ToUserId,
            UserId = Guid.Parse(userId!),
            SendDate = DateTime.UtcNow,
            IsUserRead = true,
            IsToUserRead = false
        };

        var result = await chatRepository.SendMessageAsync(chat, cancellationToken);

        if (result.IsSuccessful)
        {
            string? connectionId = ChatHub.Users.FirstOrDefault(p => p.Key == request.ToUserId).Value;
            if (connectionId is not null)
            {
                await hubContext.Clients.Client(connectionId).SendAsync("message", chat);
                User? user = await userRepository.GetByIdAsync(chat.UserId, cancellationToken);
                if (user is not null)
                {
                    int count = await chatRepository.UnReadChatMessageCount(chat.ToUserId, chat.UserId, cancellationToken);
                    ChatUserDto chatUserDto =
                        new(
                            chat.UserId,
                            user.FullName,
                            user.Avatar,
                            user.IsActive,
                            user.LastActiveDate,
                            count
                            );
                    await hubContext.Clients.Client(connectionId).SendAsync("chatUser", chatUserDto);
                }
            }


            User? toUser = await userRepository.GetByIdAsync(chat.ToUserId, cancellationToken);

            if (toUser is not null)
            {
                string? myConnectionId = ChatHub.Users.FirstOrDefault(p => p.Key == chat.UserId).Value;
                ChatUserDto chatUserDto =
                    new(
                        chat.UserId,
                        toUser.FullName,
                        toUser.Avatar,
                        toUser.IsActive,
                        toUser.LastActiveDate,
                        0
                        );
                await hubContext.Clients.Client(myConnectionId).SendAsync("chatUser", chatUserDto);
            }
        }

        return result;
    }

    public async Task<Result<List<ChatUserDto>>> GetAllChatUsers(CancellationToken cancellationToken = default)
    {
        var userId = httpContextAccessor.HttpContext?.User.Claims.FirstOrDefault(p => p.Type == "UserId")?.Value;

        List<ChatUserDto> users = await chatRepository.GetAllChatUsers(Guid.Parse(userId!), cancellationToken);

        return users;
    }
}
