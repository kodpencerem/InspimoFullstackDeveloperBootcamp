using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Repositories;
using MediatR;

namespace CleanArhitecture.Application.Features.Todos.GetAllTodo;

internal sealed class GetAllTodoQueryHandler
    (ITodoRepository todoRepository) : IRequestHandler<GetAllTodoQuery, List<GetAllTodoQueryResponse>>
{
    public async Task<List<GetAllTodoQueryResponse>> Handle(GetAllTodoQuery request, CancellationToken cancellationToken)
    {
        List<Todo> todos = await todoRepository.GetAllAsync(cancellationToken);
        List<GetAllTodoQueryResponse> response = todos.Select(s => new GetAllTodoQueryResponse(
            s.Id,
            s.Work,
            s.DeadLine,
            s.IsCompleted,
            s.Note
        )).ToList();
        return response;

        //string.Join(".",s.DeadLine.Day,s.DeadLine.Month,s.DeadLine.Year)
    }
}
