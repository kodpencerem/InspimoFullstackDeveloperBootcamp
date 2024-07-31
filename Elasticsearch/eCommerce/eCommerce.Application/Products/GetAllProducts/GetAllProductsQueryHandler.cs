using eCommerce.Domain.Entities;
using eCommerce.Domain.Repositories;
using MediatR;

namespace eCommerce.Application.Products.GetAllProducts;

internal sealed class GetAllProductsQueryHandler
    (IProductRepository productRepository) : IRequestHandler<GetAllProductsQuery, List<Product>>
{
    public async Task<List<Product>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
    {
        var response = await productRepository.GetAllAsync(cancellationToken);

        return response;
    }
}
