using AutoMapper;
using eCommerce.Domain.Entities;
using eCommerce.Domain.Repositories;
using MediatR;

namespace eCommerce.Application.Products.CreateProduct;

internal sealed class CreateProductCommandHandler(
    IProductRepository productRepository,
    IMapper mapper) : IRequestHandler<CreateProductCommand, Guid>
{
    public async Task<Guid> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        bool isNameExists = await productRepository.IsNameExistsAsync(request.Name, cancellationToken);

        if (isNameExists)
        {
            throw new ArgumentException("Product name already exists");
        }

        string filename = string.Join(DateTime.Now.ToFileTime().ToString(), request.File.FileName);

        using (var stream = File.Create($"wwwroot/images/{filename}"))
        {
            request.File.CopyTo(stream);
        }

        Product product = mapper.Map<Product>(request);
        product.CoverImage = filename;

        var response = await productRepository.CreateAsync(product, cancellationToken);

        return response;
    }
}
