using eOkulServer.Application.Features.UserTypes.CreateUserType;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace eOkulServer.WebAPI.Controllers;
[Route("api/[controller]/[action]")]
[ApiController]
public class UserTypesController(
    IMediator mediator) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create(CreateUserTypeCommand request, CancellationToken cancellationToken)
    {
        var response = await mediator.Send(request, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }
}
