using eOkulServer.Domain.Abstracts;
using eOkulServer.Domain.Entities;
using eOkulServer.Domain.Repositories;
using MediatR;

namespace eOkulServer.Application.Features.UserTypes.CreateUserType;

internal sealed class CreateUserTypeCommandHandler(
    IUserTypeRepository userTypeRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<CreateUserTypeCommand, Result<string>>
{
    public async Task<Result<string>> Handle(CreateUserTypeCommand request, CancellationToken cancellationToken)
    {
        bool isUserTypeExists = await userTypeRepository.AnyAsync(p => p.Name == request.Name, cancellationToken);

        if (isUserTypeExists)
        {
            return Result<string>.Failure("User type already exists");
        }

        UserType userType = new()
        {
            Name = request.Name
        };

        await userTypeRepository.CreateAsync(userType, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        //return Result<string>.Success(userType.Id.ToString());
        return userType.Id.ToString();
    }
}
