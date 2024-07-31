using AutoMapper;
using eCommerce.Application.Products.CreateProduct;
using eCommerce.Domain.Entities;

namespace eCommerce.Application.Mapping;
internal sealed class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CreateProductCommand, Product>();
    }
}
