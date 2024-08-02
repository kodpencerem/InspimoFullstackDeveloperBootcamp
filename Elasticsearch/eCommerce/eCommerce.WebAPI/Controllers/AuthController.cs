using eCommerce.Application.Auth.Login;
using eCommerce.Application.Auth.Register;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace eCommerce.WebAPI.Controllers;
[Route("api/[controller]/[action]")]
[ApiController]
public sealed class AuthController(
    IMediator mediator) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Register(RegisterCommand request, CancellationToken cancellationToken)
    {
        var response = await mediator.Send(request, cancellationToken);
        return Ok(new { Message = response });
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginCommand request, CancellationToken cancellationToken)
    {
        var response = await mediator.Send(request, cancellationToken);
        return Ok(response);
    }
}
