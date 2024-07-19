using AutoMapper;
using eOkulServer.Domain.Abstracts;
using eOkulServer.Domain.Entities;
using eOkulServer.Domain.Repositories;
using MediatR;

namespace eOkulServer.Application.Features.UserTypes.UpdateUserType;

internal sealed class UpdateUserTypeCommandHandler(
    IUserTypeRepository userTypeRepository,
    IUnitOfWork unitOfWork,
    IMapper mapper) : IRequestHandler<UpdateUserTypeCommand, Result<string>>
{
    public async Task<Result<string>> Handle(UpdateUserTypeCommand request, CancellationToken cancellationToken)
    {
        UserType? userType = await userTypeRepository.GetByIdAsync(request.Id, cancellationToken);
        if (userType is null)
        {
            return Result<string>.Failure("User type bulunamadı");
        }

        if (userType.Name != request.Name)
        {
            bool isUserTypeExists = await userTypeRepository.AnyAsync(p => p.Name == request.Name, cancellationToken);

            if (isUserTypeExists)
            {
                return Result<string>.Failure("User type daha önce oluşturulmuş");
            }
        }

        mapper.Map(request, userType);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return "User type güncelleme başarıyla tamamlandı";
    }
}
