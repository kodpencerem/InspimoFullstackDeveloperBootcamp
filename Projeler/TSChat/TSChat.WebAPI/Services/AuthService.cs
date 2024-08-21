using GenericFileService.Files;
using Microsoft.AspNetCore.SignalR;
using TS.Result;
using TSChat.WebAPI.DTOs;
using TSChat.WebAPI.Hubs;
using TSChat.WebAPI.Models;
using TSChat.WebAPI.Repositories;

namespace TSChat.WebAPI.Services;

public sealed class AuthService(
    IUserRepository userRepository,
    JwtProvider jwtProvider,
    IHubContext<ChatHub> hubContext
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

        HashingHelper hashingHelper = new();
        var password = hashingHelper.CreatePassword(request.Password);
        User user = new()
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            UserName = request.UserName,
            Avatar = fileName,
            PasswordHash = password.passwordHash,
            PasswordSalt = password.passwordSalt,
            Profession = request.Profession,
            LastActiveDate = DateTimeOffset.Now
        };

        var result = await userRepository.CreateAsync(user, cancellationToken);

        return result;
    }

    public async Task<Result<string>> LoginAsync(LoginDto request, CancellationToken cancellationToken = default)
    {
        User? user = await userRepository.GetUserByUserNameAndPasswordAsync(request.UserName, request.Password, cancellationToken);

        if (user is null)
        {
            return Result<string>.Failure("User name or password is wrong");
        };

        string token = jwtProvider.CreatToken(user);

        user.IsActive = true;
        user.LastActiveDate = null;

        var result = await userRepository.UpdateAsync(user, cancellationToken);

        if (!result.IsSuccessful)
        {
            return result;
        }


        object response = new
        {
            Id = user.Id,
            IsActive = user.IsActive,
            LastActiveDate = user.LastActiveDate
        };
        await hubContext.Clients.All.SendAsync("LogoutUserInformation", response);

        return token;
    }
}
