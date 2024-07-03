using CleanArhitecture.Application.Features.Todos.CreateTodo;
using CleanArhitecture.Application.Features.Todos.DeleteTodo;
using CleanArhitecture.Application.Features.Todos.GetAllTodo;
using CleanArhitecture.Application.Features.Todos.UpdateTodo;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.WebAPI.Controllers;
[Route("api/[controller]/[action]")]
[ApiController]
public class TodosController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create(CreateTodoCommand request, CancellationToken cancellationToken)
    {
        await mediator.Send(request, cancellationToken);

        return Created();
    }

    [HttpPut]
    public async Task<IActionResult> Update(UpdateTodoCommand request, CancellationToken cancellationToken)
    {
        await mediator.Send(request, cancellationToken);
        return Ok();
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteById(Guid id, CancellationToken cancellationToken)
    {
        DeleteTodoByIdCommand request = new(id);
        await mediator.Send(request, cancellationToken);
        return Ok();
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        GetAllTodoQuery request = new();
        List<GetAllTodoQueryResponse> todos = await mediator.Send(request, cancellationToken);
        return Ok(todos);
    }
}
