namespace TodoApp.WebAPI.Models;

public sealed record Todo(
    Guid Id,
    string Work,
    bool IsCompleted,
    DateTime CreatedDate);
