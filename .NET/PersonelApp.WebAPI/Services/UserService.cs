using PersonelApp.WebAPI.DTOs;
using PersonelApp.WebAPI.Models;
using PersonelApp.WebAPI.Repositories;

namespace PersonelApp.WebAPI.Services;

public sealed class UserService(
    IUserRepository userRepository) : IUserService
{
    public User? Login(LoginDto request)
    {
        if (request.UserName.Length < 3)
        {
            throw new ArgumentException("User name must be greater than 3 characters");

        }

        return userRepository.GetByUserNameAndPassword(request.UserName, request.Password);
    }

    public bool Register(RegisterDto request)
    {
        bool isUserNameExists = userRepository.IsUserNameExists(request.UserName);
        if (isUserNameExists)
        {
            throw new ArgumentException("User name already exists");
        }

        User user = new()
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            UserName = request.UserName,
            Password = request.Password,
        };

        return userRepository.Create(user);
    }
}
