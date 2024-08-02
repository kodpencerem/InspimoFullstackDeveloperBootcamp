using eCommerce.Domain.Entities;
using eCommerce.Domain.Repositories;
using Elastic.Clients.Elasticsearch;

namespace eCommerce.Infrastructure.Repositories;

internal sealed class UserElasticSearchRepository : IUserRepository, IElasticSearchRepository
{
    private readonly ElasticsearchClient _client;

    public UserElasticSearchRepository()
    {
        var settings = new ElasticsearchClientSettings(new Uri("http://localhost:9200"))
            .DefaultIndex("users");
        _client = new ElasticsearchClient(settings);
        CreateIndexAsync("users", default).Wait();
    }
    public async Task CreateIndexAsync(string indexName, CancellationToken cancellationToken = default)
    {
        await _client.Indices.CreateAsync<User>(indexName);
    }

    public async Task<Guid> CreateAsync(User user, CancellationToken cancellationToken = default)
    {
        IndexResponse indexResponse = await _client.IndexAsync(user, cancellationToken);

        return Guid.Parse(indexResponse.Id);
    }

    public async Task<User?> FindByUserNameAndPasswordAsync(string userName, string password, CancellationToken cancellationToken = default)
    {
        var response = await _client.SearchAsync<User>(s => s
            .Index("users")
            .Query(q => q.Term(t => t
                                    .Field(f => f.UserName.Suffix("keyword"))
                                    .Value(userName)
                                    .Field(f => f.Password.Suffix("keyword"))
                                    .Value(password)

                              )
                  )
        );

        return response.Documents.FirstOrDefault();
    }

    public async Task<bool> IsUserNameExistsAsync(string userName, CancellationToken cancellationToken = default)
    {
        var response = await _client.SearchAsync<User>(s => s
            .Index("users")
            .Query(q => q.Term(t => t
                                    .Field(f => f.UserName.Suffix("keyword"))
                                    .Value(userName)

                              )
                  )
        );

        return response.Hits.Count > 0;
    }

    public async Task<bool> IsEmailExistsAsync(string email, CancellationToken cancellationToken = default)
    {
        var response = await _client.SearchAsync<User>(s => s
            .Index("users")
            .Query(q => q.Term(t => t
                                    .Field(f => f.Email.Suffix("keyword"))
                                    .Value(email)

                              )
                  )
        );

        return response.Hits.Count > 0;
    }
}
