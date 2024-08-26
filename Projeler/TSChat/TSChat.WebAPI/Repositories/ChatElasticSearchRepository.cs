using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.QueryDsl;
using TS.Result;
using TSChat.WebAPI.DTOs;
using TSChat.WebAPI.Models;

namespace TSChat.WebAPI.Repositories;

public sealed class ChatElasticSearchRepository : IChatRepository
{
    ElasticsearchClient client;

    public ChatElasticSearchRepository()
    {
        ElasticsearchClientSettings settings = new(new Uri("http://localhost:9200"));
        settings.DefaultIndex("chats");

        client = new ElasticsearchClient(settings);

        IndexRequest<Chat> indexRequest = new("chats");
        client.IndexAsync(indexRequest).GetAwaiter().GetResult();
    }
    public async Task<Result<List<Chat>>> GetAllAsync(Guid userId, Guid toUserId, CancellationToken cancellationToken = default)
    {
        SearchRequest searchRequest = new(Indices.Index("chats"))
        {
            Size = 10000,
            Query = new BoolQuery
            {
                Should = new List<Query>
                {
                    // userId == userId && toUserId == toUserId
                    new BoolQuery
                    {
                        Should = new List<Query>
                        {
                            new MatchQuery(new Field("userId"))
                            {
                                Query = userId
                            },
                            new MatchQuery(new Field("toUserId"))
                            {
                                Query = toUserId
                            }
                        }
                    },
                    // toUserId == userId && userId == toUserId
                    new BoolQuery
                    {
                        Must = new List<Query>
                        {
                            new MatchQuery(new Field("userId"))
                            {
                                Query = toUserId
                            },
                            new MatchQuery(new Field("toUserId"))
                            {
                                Query = userId
                            }
                        }
                    }
                }
            }
        };

        SearchResponse<Chat> searchResponse = await client.SearchAsync<Chat>(searchRequest, cancellationToken);

        return searchResponse.Documents.ToList();
    }

    public Task<List<ChatUserDto>> GetAllChatUsers(Guid userId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<Result<string>> SendMessageAsync(Chat chat, CancellationToken cancellationToken = default)
    {
        CreateRequest<Chat> createRequest = new("chats", chat.Id)
        {
            Document = chat,
            Refresh = Refresh.True
        };

        CreateResponse createResponse = await client.CreateAsync(createRequest, cancellationToken);

        if (!createResponse.IsSuccess())
        {
            return Result<string>.Failure("Something went wrong");
        }

        return "Message sent successfully";
    }

    public Task<int> UnReadChatMessageCount(Guid userId, Guid toUserId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
