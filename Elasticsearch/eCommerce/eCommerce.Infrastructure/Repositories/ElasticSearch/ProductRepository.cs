using eCommerce.Domain.Entities;
using eCommerce.Domain.Repositories;
using Elastic.Clients.Elasticsearch;

namespace eCommerce.Infrastructure.Repositories.ElasticSearch;
internal sealed class ProductRepository : IProductRepository, IElasticSearchRepository
{
    private readonly ElasticsearchClient _client;

    public ProductRepository()
    {
        var settings = new ElasticsearchClientSettings(
            new Uri("http://localhost:9200"))
            .DefaultIndex("products");

        _client = new ElasticsearchClient(settings);
        CreateIndexAsync("products", default).Wait();
    }
    public async Task CreateIndexAsync(string indexName, CancellationToken cancellationToken = default)
    {
        await _client.Indices.CreateAsync<Product>("products");
    }

    public async Task<Guid> CreateAsync(Product product, CancellationToken cancellationToken = default)
    {
        IndexResponse indexResponse = await _client.IndexAsync(product, cancellationToken);

        return Guid.Parse(indexResponse.Id);
    }

    public async Task<List<Product>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var response = await _client.SearchAsync<Product>(s =>
            s.Index("products").Size(1000));

        if (response.IsSuccess())
        {
            var doc = response.Documents.ToList();
            return doc;
        }

        return new List<Product>();
    }

    public async Task<bool> IsNameExistsAsync(string name, CancellationToken cancellationToken)
    {
        var response = await _client.SearchAsync<Product>(s => s
            .Index("products")
            .Query(q => q.Term(t => t
                                    .Field(f => f.Name.Suffix("keyword"))
                                    .Value(name)
                              )
                  )
        );

        return response.Hits.Count > 0;
    }
}
