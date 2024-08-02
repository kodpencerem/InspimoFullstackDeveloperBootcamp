using eCommerce.Domain.Entities;
using eCommerce.Domain.Repositories;
using MediatR;

namespace eCommerce.Application.Auth.Register;

internal sealed class RegisterCommandHandler(
    IUserRepository userRepository) : IRequestHandler<RegisterCommand, string>
{
    public async Task<string> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        bool isUserNameExists = await userRepository.IsUserNameExistsAsync(request.UserName, cancellationToken);

        if (isUserNameExists)
        {
            throw new ArgumentException("User name already exists");
        }

        bool isEmailExists = await userRepository.IsEmailExistsAsync(request.Email, cancellationToken);

        if (isEmailExists)
        {
            throw new ArgumentException("Email already exists");
        }

        User user = new()
        {
            UserName = request.UserName,
            Email = request.Email,
            Password = request.Password,
            FirstName = request.FirstName,
            LastName = request.LastName,
        };

        Guid id = await userRepository.CreateAsync(user, cancellationToken);

        return "Register is successful";
    }
}
