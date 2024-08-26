namespace TSChat.WebAPI.DTOs;

public sealed record ChatUserDto(
    Guid Id,
    string FullName,
    string Avatar,
    bool IsActive,
    DateTime? LastActiveDate,
    int UnReadMessageCount,
    string IsActiveInformation = "");
