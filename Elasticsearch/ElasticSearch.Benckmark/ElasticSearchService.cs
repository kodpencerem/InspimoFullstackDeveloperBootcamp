using Nest;

namespace ElasticSearch.Benckmark;
internal class ElasticSearchService
{
    private readonly ElasticClient _client;

    public ElasticSearchService()
    {
        var settings = new ConnectionSettings(new Uri("http://localhost:9200"))
            .DefaultIndex("products");

        _client = new ElasticClient(settings);
    }

    public void CreateIndex()
    {
        var indexResponse = _client.Indices.Create("products", c =>
            c.Map<Product>(m => m.AutoMap()));
    }


    public IndexResponse Add(Product product)
    {
        var indexResponse = _client.IndexDocument(product);
        return indexResponse;
    }

    public List<Product> GetAll(string description)
    {
        //var response = _client.Search<Product>(s =>
        //      s.Query(q =>
        //          q.QueryString(m => m
        //             .DefaultField(f => f.Description)
        //             .Query("*" + description + "*")
        //          )
        //      )
        //      .Size(1000)
        //  );

        var response = _client.Search<Product>(s => s.Size(1000));

        return response.Documents.ToList();
    }
}


public sealed class Product
{
    public Product()
    {
        Id = Guid.NewGuid();
    }
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public decimal Price { get; set; }
}