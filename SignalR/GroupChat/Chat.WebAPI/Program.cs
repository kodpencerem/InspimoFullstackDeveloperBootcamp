using Chat.WebAPI.Hubs;
using Microsoft.AspNetCore.SignalR;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors();

builder.Services.AddSignalR();

var app = builder.Build();

app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().AllowCredentials().SetIsOriginAllowed(x => true));

app.MapGet("/", () => "Hello World!");

app.MapGet("/send-message", async (string name, string message, IHubContext<ChatHub> hubContext) =>
{
    await hubContext.Clients.All.SendAsync("msg", new { Name = name, Message = message });
    return Results.Created();
});

app.MapGet("/send-group-message", async (string name, string message, IHubContext<ChatHub> hubContext) =>
{
    await hubContext.Clients.Groups("group1").SendAsync("grpmsg", new { Name = name, Message = message });

    return Results.Created();
});

app.MapHub<ChatHub>("/chat");

app.Run();
