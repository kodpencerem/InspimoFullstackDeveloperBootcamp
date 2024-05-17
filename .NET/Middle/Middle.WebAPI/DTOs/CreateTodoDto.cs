namespace Middle.WebAPI.DTOs;

public sealed record CreateTodoDto(
    int UserId,
    string Title,
    bool Completed);

//public sealed class CreateTodoDto2 { //bu recordun class hali
//    public CreateTodoDto2(int userId, string title, bool completed)
//    {
//        UserId = userId;
//        Title = title;
//        Completed = completed;
//    }
//    public int UserId { get; init; }
//    public string Title { get; init; }
//    public bool Completed { get; init; }
//}
