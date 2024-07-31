using eCommerce.Domain.Entities;

namespace eCommerce.Domain.Repositories;
public interface IProductRepository
{
    Task<Guid> CreateAsync(Product product, CancellationToken cancellationToken = default);
    Task<List<Product>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<bool> IsNameExistsAsync(string name, CancellationToken cancellationToken);
}
