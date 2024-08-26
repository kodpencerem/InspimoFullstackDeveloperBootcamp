using Microsoft.EntityFrameworkCore;
using TS.Result;
using TSChat.WebAPI.Context;
using TSChat.WebAPI.DTOs;
using TSChat.WebAPI.Models;

namespace TSChat.WebAPI.Repositories;

public sealed class ChatEFCoreRepository(
    ApplicationDbContext context) : IChatRepository
{
    public async Task<Result<List<Chat>>> GetAllAsync(Guid userId, Guid toUserId, CancellationToken cancellationToken = default)
    {
        var response =
            await context.Chats
            .Where(p => (p.UserId == userId && p.ToUserId == toUserId) || p.UserId == toUserId && p.ToUserId == userId)
            .OrderBy(p => p.SendDate)
            .ToListAsync(cancellationToken);

        await context.Chats
            .Where(p => p.ToUserId == userId && p.UserId == toUserId && p.IsToUserRead == false)
            .ExecuteUpdateAsync(p => p.SetProperty(c => c.IsToUserRead, true));

        return response;
    }

    public async Task<List<ChatUserDto>> GetAllChatUsers(Guid userId, CancellationToken cancellationToken)
    {
        List<Guid> toUserIds =
            await context.Chats
            .Where(p => p.UserId == userId)
            .GroupBy(p => p.ToUserId)
            .Select(s => s.Key)
            .ToListAsync(cancellationToken);

        List<Guid> userIds =
            await context.Chats
            .Where(p => p.ToUserId == userId)
            .GroupBy(p => p.UserId)
            .Select(s => s.Key)
            .ToListAsync(cancellationToken);

        HashSet<Guid> ids = new();
        ids.UnionWith(toUserIds);
        ids.UnionWith(userIds);

        List<ChatUserDto> response = new();

        foreach (var id in ids)
        {
            User user = await context.Users.FirstAsync(p => p.Id == id, cancellationToken);

            int count = await context.Chats.CountAsync(p => p.ToUserId == userId && p.UserId == id && p.IsToUserRead == false, cancellationToken);

            ChatUserDto chatUserDto =
                new(
                    id,
                    user.FullName,
                    user.Avatar,
                    user.IsActive,
                    user.LastActiveDate,
                    count
                    );
            response.Add(chatUserDto);
        }

        return response;
    }

    public async Task<Result<string>> SendMessageAsync(Chat chat, CancellationToken cancellationToken = default)
    {
        await context.AddAsync(chat, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        return "Message send is successful";
    }

    public async Task<int> UnReadChatMessageCount(Guid userId, Guid toUserId, CancellationToken cancellationToken = default)
    {
        return await context.Chats.CountAsync(p => p.ToUserId == userId && p.UserId == toUserId && p.IsToUserRead == false, cancellationToken);
    }
}
