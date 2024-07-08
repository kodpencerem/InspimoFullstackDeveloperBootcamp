using MediatR;

namespace eOkulServer.Application.Features.UserTypes.CreateUserType;
public sealed record CreateUserTypeCommand(
    string Name) : IRequest;