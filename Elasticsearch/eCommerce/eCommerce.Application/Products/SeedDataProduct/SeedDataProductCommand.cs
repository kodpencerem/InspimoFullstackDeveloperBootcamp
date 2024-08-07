using MediatR;

namespace eCommerce.Application.Products.SeedDataProduct;
public sealed record SeedDataProductCommand() : IRequest<string>;