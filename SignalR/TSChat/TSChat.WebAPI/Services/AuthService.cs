using GenericFileService.Files;
using TS.Result;
using TSChat.WebAPI.DTOs;
using TSChat.WebAPI.Models;
using TSChat.WebAPI.Repositories;

namespace TSChat.WebAPI.Services;

public sealed class AuthService(
    IUserRepository userRepository
    )
{
    public async Task<Result<string>> RegisterAsync(RegisterDto request, CancellationToken cancellationToken)
    {
        bool isUserNameExists = await userRepository.IsUserNameExist(request.UserName, cancellationToken);
        if (isUserNameExists)
        {
            return Result<string>.Failure("Username already exist");
        }

        string fileName = FileService.FileSaveToServer(request.File, "wwwroot/avatars/");

        User user = new()
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            UserName = request.UserName,
            Avatar = fileName,
            Password = request.Password
        };

        var result = await userRepository.CreateAsync(user, cancellationToken);

        return result;
    }

    public async Task LoginAsync()
    {
        await Task.CompletedTask;
    }
}
