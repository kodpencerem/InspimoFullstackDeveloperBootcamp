using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.QueryDsl;
using TS.Result;
using TSChat.WebAPI.Models;
using TSChat.WebAPI.Services;

namespace TSChat.WebAPI.Repositories;

public sealed class UserElasticSearchRepository : IUserRepository
{
    private readonly ElasticsearchClient client;
    public UserElasticSearchRepository()
    {
        ElasticsearchClientSettings settings = new(new Uri("http://localhost:9200"));
        settings.DefaultIndex("users");

        client = new ElasticsearchClient(settings);

        IndexRequest<User> indexRequest = new("users");
        client.IndexAsync<User>(indexRequest).GetAwaiter().GetResult();
    }
    public async Task<Result<string>> CreateAsync(User user, CancellationToken cancellationToken = default)
    {
        CreateRequest<User> createRequest = new("users", user.Id)
        {
            Document = user,
            Refresh = Refresh.True
        };
        CreateResponse createResponse = await client.CreateAsync<User>(createRequest, cancellationToken);
        if (!createResponse.IsSuccess())
        {
            return Result<string>.Failure("Something went wrong");
        }

        return "Create is successful";
    }

    public async Task<bool> IsUserNameExist(string userName, CancellationToken cancellationToken = default)
    {
        SearchRequest searchRequest = new(Indices.Index("users"))
        {
            Size = 1,
            Query = new TermQuery(new Field("userName"))
            {
                Value = userName
            }
        };
        SearchResponse<User> searchResponse = await client.SearchAsync<User>(searchRequest, cancellationToken);
        return searchResponse.Documents.Count() > 0;
    }

    public async Task<User?> GetUserByUserNameAndPasswordAsync(string userName, string password, CancellationToken cancellationToken = default)
    {
        SearchRequest searchRequest = new(Indices.Index("users"))
        {
            Query = new TermQuery(new Field("userName"))
            {
                Value = userName
            },
            Size = 1
        };

        SearchResponse<User> searchResponse = await client.SearchAsync<User>(searchRequest, cancellationToken);

        User? user = searchResponse.Documents.FirstOrDefault();

        if (user is null) return null;

        HashingHelper hashingHelper = new();
        bool verifyPassword = hashingHelper.VerifyPasswordHash(password, user.PasswordSalt, user.PasswordHash);

        if (!verifyPassword) return null;

        return user;
    }

    public async Task<Result<string>> UpdateAsync(User user, CancellationToken cancellationToken = default)
    {
        UpdateRequest<User, User> updateRequest = new("users", user.Id)
        {
            Doc = user,
            Refresh = Refresh.True
        };

        UpdateResponse<User> updateResponse = await client.UpdateAsync(updateRequest, cancellationToken);

        if (!updateResponse.IsSuccess())
        {
            return Result<string>.Failure("Something went wrong");
        }

        return "Update is successful";
    }

    public async Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        GetRequest getRequest = new("users", id);
        GetResponse<User> getResponse = await client.GetAsync<User>(getRequest, cancellationToken);
        User? user = getResponse.Source;

        if (user is null) return null;

        return user;
    }

    public async Task<Result<List<User>>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        SearchRequest searchRequest = new(Indices.Index("users"))
        {
            Query = new MatchAllQuery(),
            Size = 1000
        };

        SearchResponse<User> searchResponse = await client.SearchAsync<User>(searchRequest, cancellationToken);

        if (!searchResponse.IsSuccess())
        {
            return Result<List<User>>.Failure("Something went wrong");
        }

        return searchResponse.Documents.ToList();
    }
}
