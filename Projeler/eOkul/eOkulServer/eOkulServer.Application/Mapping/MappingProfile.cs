using AutoMapper;
using eOkulServer.Application.Features.UserTypes.CreateUserType;
using eOkulServer.Application.Features.UserTypes.UpdateUserType;
using eOkulServer.Domain.Entities;

namespace eOkulServer.Application.Mapping;
internal sealed class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CreateUserTypeCommand, UserType>();
        CreateMap<UpdateUserTypeCommand, UserType>();
    }
}
