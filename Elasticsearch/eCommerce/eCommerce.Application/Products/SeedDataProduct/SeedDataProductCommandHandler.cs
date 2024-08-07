using Bogus;
using eCommerce.Domain.Entities;
using eCommerce.Domain.Repositories;
using MediatR;

namespace eCommerce.Application.Products.SeedDataProduct;

internal sealed class SeedDataProductCommandHandler(
    IProductRepository productRepository) : IRequestHandler<SeedDataProductCommand, string>
{
    public async Task<string> Handle(SeedDataProductCommand request, CancellationToken cancellationToken)
    {
        for (int i = 0; i < 1000; i++)
        {
            Faker faker = new();
            Product product = new()
            {
                Name = faker.Commerce.ProductName(),
                Description = faker.Commerce.ProductDescription(),
                CoverImage = faker.Person.Avatar,
                Price = Convert.ToDecimal(faker.Commerce.Price(10, 1000, 2)),
                Stock = faker.Commerce.Random.Int(1, 100)
            };

            await productRepository.CreateAsync(product, cancellationToken);
        }

        return "Seed data is successful";
    }
}
