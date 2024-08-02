using MediatR;

namespace eCommerce.Application.Auth.Register;
public sealed record RegisterCommand(
    string FirstName,
    string LastName,
    string UserName,
    string Email,
    string Password) : IRequest<string>;
