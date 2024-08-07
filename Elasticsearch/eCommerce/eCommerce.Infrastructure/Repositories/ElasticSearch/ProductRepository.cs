using eCommerce.Application.Services;
using eCommerce.Domain.Entities;
using eCommerce.Domain.Repositories;
using Elastic.Clients.Elasticsearch;

namespace eCommerce.Infrastructure.Repositories.ElasticSearch;
internal sealed class ProductRepository : IProductRepository, IElasticSearchRepository
{
    private readonly ElasticsearchClient _client;
    private readonly ICacheService _cache;
    public ProductRepository(ICacheService cache)
    {
        var settings = new ElasticsearchClientSettings(
            new Uri("http://localhost:9200"))
            .DefaultIndex("products");

        _client = new ElasticsearchClient(settings);
        CreateIndexAsync("products", default).Wait();
        _cache = cache;
    }
    public async Task CreateIndexAsync(string indexName, CancellationToken cancellationToken = default)
    {
        await _client.Indices.CreateAsync<Product>("products");
    }

    public async Task<Guid> CreateAsync(Product product, CancellationToken cancellationToken = default)
    {
        CreateRequest<Product> createRequest = new("products", product.Id)
        {
            Document = product
        };

        CreateResponse createResponse = await _client.CreateAsync(createRequest, cancellationToken);

        if (createResponse.IsSuccess())
        {
            _cache.Remove("products");
        }

        return Guid.Parse(createResponse.Id);
    }

    public async Task<List<Product>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        _cache.Get("products", out List<Product>? products);

        if (products is null)
        {
            var response = await _client.SearchAsync<Product>(s =>
            s.Index("products").Size(1000));

            if (response.IsSuccess())
            {
                products = response.Documents.ToList();

                _cache.Set("products", products);
            }
        }

        return products!;
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