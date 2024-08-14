using Microsoft.AspNetCore.SignalR;

namespace Chat.WebAPI.Hubs;

public class ChatHub : Hub
{
    public static Dictionary<string, string> GroupMembers = new();

    public async Task JoinGroup(string name)
    {
        GroupMembers.Add(Context.ConnectionId, name);
        await Groups.AddToGroupAsync(Context.ConnectionId, "group1");
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        GroupMembers.Remove(Context.ConnectionId);
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, "group1");
    }
}
