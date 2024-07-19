using eOkulServer.Domain.Abstracts;
using MediatR;

namespace eOkulServer.Application.Features.Categories.UpdateCategory;
public sealed record UpdateCategoryCommand(
    Guid Id,
    string Name) : IRequest<Result<string>>;
