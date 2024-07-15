using eOkulServer.Domain.Abstracts;
using MediatR;

namespace eOkulServer.Application.Features.UserTypes.UpdateUserType;
public sealed record UpdateUserTypeCommand(
    Guid Id,
    string Name) : IRequest<Result<string>>;
