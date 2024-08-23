namespace TSChat.WebAPI.DTOs;

public sealed record SendMessageDto(
    Guid ToUserId,
    string Message);
