namespace TSChat.WebAPI.DTOs;

public sealed record RegisterDto(
    string FirstName,
    string LastName,
    string UserName,
    string Password,
    string Profession,
    IFormFile File);
