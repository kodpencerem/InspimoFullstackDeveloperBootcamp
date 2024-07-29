using Elastic.Clients.Elasticsearch;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
//Service registration
builder.Services.AddSwaggerGen();
builder.Services.AddEndpointsApiExplorer();
//Service registration
var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

//middleware
app.MapGet("/", () => "Hello World!");

app.MapPost("/personels/create", async (Personel request) =>
{
    var client = new ElasticsearchClient();

    ConnectionSettings.DefaultMappingFor<TDocument>();
    await client.IndexAsync(request);

    return Results.Created();
});

app.MapGet("/personels/getall", async () =>
{
    var client = new ElasticsearchClient();

    var response = await client.SearchAsync<Personel>(s =>
    s.Index("personel"));

    return response.Documents;
});

app.Run();
//middleware

class Personel
{
    [JsonPropertyName("firstName")]
    public string FirstName { get; set; } = default!;
    [JsonPropertyName("lastName")]
    public string LastName { get; set; } = default!;
    [JsonPropertyName("salary")]
    public decimal Salary { get; set; }
    [JsonPropertyName("dateOfBirth")]
    public DateOnly DateOfBirth { get; set; }
}