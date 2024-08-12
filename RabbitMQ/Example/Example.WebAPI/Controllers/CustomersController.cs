using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.QueryDsl;
using Example.WebAPI.Context;
using Example.WebAPI.DTOs;
using Example.WebAPI.Models;
using Example.WebAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace Example.WebAPI.Controllers;
[Route("api/[controller]/[action]")]
[ApiController]
public class CustomersController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly ElasticsearchClient _client;
    public CustomersController(ApplicationDbContext context)
    {
        _context = context;

        ElasticsearchClientSettings settings = new ElasticsearchClientSettings(new Uri("http://localhost:9200"));
        settings.DefaultIndex("customers");

        _client = new ElasticsearchClient(settings);
        _client.IndexAsync("customers").GetAwaiter();
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        SearchRequest searchRequest = new SearchRequest(Indices.Index("customers"))
        {
            Size = 10000,
            Query = new MatchAllQuery()
        };
        SearchResponse<Customer> searchResponse = await _client.SearchAsync<Customer>(searchRequest, cancellationToken);

        return Ok(searchResponse.Documents);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateCustomerDto request, CancellationToken cancellationToken)
    {
        Customer customer = new()
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
        };

        await _context.AddAsync(customer, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        RabbitMQService.CreateDataToElasticSearch(customer);

        return Created();
    }
}
