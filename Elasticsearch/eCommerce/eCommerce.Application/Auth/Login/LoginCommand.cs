using MediatR;

namespace eCommerce.Application.Auth.Login;
public sealed record LoginCommand(
    string UserName,
    string Password) : IRequest<LoginCommandResponse>;