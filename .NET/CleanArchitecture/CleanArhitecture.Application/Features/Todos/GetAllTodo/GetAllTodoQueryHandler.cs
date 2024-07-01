using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Repositories;
using MediatR;

namespace CleanArhitecture.Application.Features.Todos.GetAllTodo;

internal sealed class GetAllTodoQueryHandler
    (ITodoRepository todoRepository) : IRequestHandler<GetAllTodoQuery, List<Todo>>
{
    public async Task<List<Todo>> Handle(GetAllTodoQuery request, CancellationToken cancellationToken)
    {
        List<Todo> todos = await todoRepository.GetAllAsync(cancellationToken);
        return todos;
    }
}
