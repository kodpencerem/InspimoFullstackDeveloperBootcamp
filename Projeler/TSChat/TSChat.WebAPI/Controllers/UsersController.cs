using Microsoft.AspNetCore.Mvc;
using TSChat.WebAPI.Services;

namespace TSChat.WebAPI.Controllers;
[Route("api/[controller]/[action]")]
[ApiController]
public sealed class UsersController(
    UserService userService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var result = await userService.GetAllAsync(cancellationToken);

        return StatusCode(result.StatusCode, result);
    }

    [HttpGet]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        var result = await userService.GetByIdAsync(id, cancellationToken);

        return StatusCode(result.StatusCode, result);
    }
}
