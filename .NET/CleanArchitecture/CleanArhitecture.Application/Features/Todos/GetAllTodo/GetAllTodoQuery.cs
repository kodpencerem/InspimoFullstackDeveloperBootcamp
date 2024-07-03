using MediatR;

namespace CleanArhitecture.Application.Features.Todos.GetAllTodo;
public sealed record GetAllTodoQuery() : IRequest<List<GetAllTodoQueryResponse>>;
