using Microsoft.AspNetCore.Mvc;
using TSChat.WebAPI.DTOs;
using TSChat.WebAPI.Services;

namespace TSChat.WebAPI.Controllers;
[Route("api/[controller]/[action]")]
[ApiController]
public class ChatsController(
    ChatService chatService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll(Guid toUserId, CancellationToken cancellationToken)
    {
        var result = await chatService.GetAllAsync(toUserId, cancellationToken);

        return StatusCode(result.StatusCode, result);
    }

    [HttpPost]
    public async Task<IActionResult> SendMessage(SendMessageDto request, CancellationToken cancellationToken)
    {
        var result = await chatService.SendMessageAsync(request, cancellationToken);

        return StatusCode(result.StatusCode, result);
    }

    [HttpGet]
    public async Task<IActionResult> GetGetAllChatUsers(CancellationToken cancellationToken)
    {
        var result = await chatService.GetAllChatUsers(cancellationToken);

        return StatusCode(result.StatusCode, result);
    }

}
