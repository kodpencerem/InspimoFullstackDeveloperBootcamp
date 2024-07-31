using eCommerce.Domain.Entities;
using MediatR;

namespace eCommerce.Application.Products.GetAllProducts;
public sealed record GetAllProductsQuery() : IRequest<List<Product>>;
