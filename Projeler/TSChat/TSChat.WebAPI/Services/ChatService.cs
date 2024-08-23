using Microsoft.AspNetCore.SignalR;
using TS.Result;
using TSChat.WebAPI.DTOs;
using TSChat.WebAPI.Hubs;
using TSChat.WebAPI.Models;
using TSChat.WebAPI.Repositories;

namespace TSChat.WebAPI.Services;

public sealed class ChatService(
    IChatRepository chatRepository,
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
            SendDate = DateTime.Now
        };

        var result = await chatRepository.SendMessageAsync(chat, cancellationToken);

        if (result.IsSuccessful)
        {
            string? connectionId = ChatHub.Users.FirstOrDefault(p => p.Key == request.ToUserId).Value;
            if (connectionId is not null)
            {
                await hubContext.Clients.Client(connectionId).SendAsync("message", chat);
            }
        }

        return result;
    }
}
