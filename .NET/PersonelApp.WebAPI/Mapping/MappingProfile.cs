using AutoMapper;
using PersonelApp.WebAPI.DTOs;
using PersonelApp.WebAPI.Models;

namespace PersonelApp.WebAPI.Mapping;

public sealed class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CreatePersonelDto, Personel>();
    }
}
