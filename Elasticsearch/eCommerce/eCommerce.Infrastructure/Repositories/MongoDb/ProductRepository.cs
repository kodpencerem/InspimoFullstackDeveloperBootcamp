using eCommerce.Domain.Entities;
using eCommerce.Domain.Repositories;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace eCommerce.Infrastructure.Repositories.MongoDb;

internal sealed class ProductRepository : IProductRepository
{
    private readonly IMongoCollection<Product> _product;

    public ProductRepository(IConfiguration configuration)
    {
        var client = new MongoClient(configuration.GetConnectionString("MongoDb"));
        var database = client.GetDatabase(configuration.GetSection("MongoDbOptions:DbName").Value);
        _product = database.GetCollection<Product>("products");
    }

    public async Task<Guid> CreateAsync(Product product, CancellationToken cancellationToken = default)
    {
        await _product.InsertOneAsync(product, null, cancellationToken);
        return product.Id;
    }

    public async Task<List<Product>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _product.Find<Product>(p => true).ToListAsync(cancellationToken);
    }

    public async Task<bool> IsNameExistsAsync(string name, CancellationToken cancellationToken)
    {
        return await _product.Find(p => p.Name == name).AnyAsync(cancellationToken);
    }
}