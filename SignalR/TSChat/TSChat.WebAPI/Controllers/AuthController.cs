using Microsoft.AspNetCore.Mvc;
using TSChat.WebAPI.DTOs;
using TSChat.WebAPI.Services;

namespace TSChat.WebAPI.Controllers;
[Route("api/[controller]/[action]")]
[ApiController]
public class AuthController(
    AuthService authService) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Register([FromForm] RegisterDto request, CancellationToken cancellationToken)
    {
        var result = await authService.RegisterAsync(request, cancellationToken);
        return StatusCode(result.StatusCode, result);
    }
}
