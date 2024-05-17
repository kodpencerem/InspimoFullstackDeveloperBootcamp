namespace Middle.WebAPI.DTOs;

public sealed record UpdateTodoDto(
    int Id,
    int UserId,
    string Title,
    bool Completed);

