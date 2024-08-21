using Microsoft.AspNetCore.SignalR;
using TSChat.WebAPI.Models;
using TSChat.WebAPI.Repositories;

namespace TSChat.WebAPI.Hubs;

public sealed class ChatHub(
    IUserRepository userRepository) : Hub
{
    public async Task Logout(Guid id)
    {
        User? user = await userRepository.GetByIdAsync(id, default);

        if (user is not null)
        {
            user.IsActive = false;
            user.LastActiveDate = DateTimeOffset.Now;

            await userRepository.UpdateAsync(user, default);

            object response = new
            {
                Id = user.Id,
                IsActive = user.IsActive,
                LastActiveDate = user.LastActiveDate
            };

            await Clients.All.SendAsync("LogoutUserInformation", response);
        }
    }
}
