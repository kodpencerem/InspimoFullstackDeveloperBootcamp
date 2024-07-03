namespace CleanArhitecture.Application.Features.Todos.GetAllTodo;

public sealed record GetAllTodoQueryResponse(
    Guid Id,
    string Work,
    DateOnly DeadLine,
    bool IsCompleted,
    string? Note);