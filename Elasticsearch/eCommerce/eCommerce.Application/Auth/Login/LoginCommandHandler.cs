using eCommerce.Application.Services;
using eCommerce.Domain.Entities;
using eCommerce.Domain.Repositories;
using MediatR;

namespace eCommerce.Application.Auth.Login;

internal sealed class LoginCommandHandler(
    IUserRepository userRepository,
    IJwtProvider jwtProvider
    ) : IRequestHandler<LoginCommand, LoginCommandResponse>
{
    public async Task<LoginCommandResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        User? user = await userRepository.FindByUserNameAndPasswordAsync(request.UserName, request.Password, cancellationToken);

        if (user is null)
        {
            throw new ArgumentException("User name or password is wrong");
        }

        string token = jwtProvider.CreateToken(user);

        LoginCommandResponse response = new(user.Id, token);

        return response;
    }
}
