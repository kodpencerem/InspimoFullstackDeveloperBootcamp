using MediatR;
using Microsoft.AspNetCore.Http;

namespace eCommerce.Application.Products.CreateProduct;
public sealed record CreateProductCommand(
    string Name,
    string Description,
    decimal Price,
    int Stock,
    IFormFile File) : IRequest<Guid>;