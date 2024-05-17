namespace Middle.WebAPI.Models;

public sealed class Todo
{
    public int Id { get; set; } //id
    public int UserId { get; set; }
    public string Title { get; set; } = string.Empty;
    public bool Completed { get; set; }
}
