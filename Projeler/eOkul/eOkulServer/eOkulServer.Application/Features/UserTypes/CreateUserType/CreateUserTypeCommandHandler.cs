using eOkulServer.Domain.Entities;
using eOkulServer.Domain.Repositories;
using MediatR;

namespace eOkulServer.Application.Features.UserTypes.CreateUserType;

internal sealed class CreateUserTypeCommandHandler(
    IUserTypeRepository userTypeRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<CreateUserTypeCommand>
{
    public async Task Handle(CreateUserTypeCommand request, CancellationToken cancellationToken)
    {
        bool isUserTypeExists = await userTypeRepository.AnyAsync(p => p.Name == request.Name, cancellationToken);

        if (isUserTypeExists)
        {
            throw new ArgumentException("User type already exists");
        }

        UserType userType = new()
        {
            Name = request.Name
        };

        await userTypeRepository.CreateAsync(userType, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
