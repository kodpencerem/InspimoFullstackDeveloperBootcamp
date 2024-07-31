namespace eCommerce.Domain.Repositories;

public interface IElasticSearchRepository
{
    Task CreateIndexAsync(string indexName, CancellationToken cancellationToken = default);
}
