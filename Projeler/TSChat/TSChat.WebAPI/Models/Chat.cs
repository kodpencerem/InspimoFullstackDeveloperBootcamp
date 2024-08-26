namespace TSChat.WebAPI.Models;

public sealed class Chat
{
    public Chat()
    {
        Id = Guid.NewGuid();
    }
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid ToUserId { get; set; }
    public DateTime SendDate { get; set; }
    public string Message { get; set; } = default!;
    public bool IsUserRead { get; set; }
    public bool IsToUserRead { get; set; }
}
