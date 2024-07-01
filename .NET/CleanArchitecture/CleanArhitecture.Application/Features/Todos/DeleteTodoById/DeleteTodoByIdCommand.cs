using MediatR;

namespace CleanArhitecture.Application.Features.Todos.DeleteTodo;
public sealed record DeleteTodoByIdCommand(
    Guid Id) : IRequest;
