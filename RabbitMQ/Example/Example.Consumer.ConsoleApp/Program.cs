using Elastic.Clients.Elasticsearch;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

var factory = new ConnectionFactory() { HostName = "localhost" };
var connection = factory.CreateConnection();
var channel = connection.CreateModel();

channel.QueueDeclare(
            queue: "elastic",
            durable: true,
            exclusive: false,
            autoDelete: false,
            arguments: null);

var consumer = new EventingBasicConsumer(channel);
consumer.Received += (model, ea) =>
{
    Console.WriteLine("Kuyruk dinlenmeye başlandı");
    var body = ea.Body.ToArray();
    string stringBody = Encoding.UTF8.GetString(body);
    var customer = JsonSerializer.Deserialize<Customer>(body);

    Console.WriteLine("Customer verisi kuyruktan alındı");
    Console.WriteLine("Customer verisi dbye işleniyor");

    ElasticsearchClientSettings settings = new ElasticsearchClientSettings(new Uri("http://localhost:9200"));
    settings.DefaultIndex("customers");

    ElasticsearchClient _client = new ElasticsearchClient(settings);
    _client.IndexAsync("customers").GetAwaiter().GetResult();

    CreateRequest<Customer> createRequest = new("customers", customer!.Id)
    {
        Document = customer
    };
    _client.CreateAsync(createRequest).GetAwaiter().GetResult();

    Console.WriteLine("Customer verisi dbye işlenme bitti");
};

channel.BasicConsume(
    queue: "elastic",
    autoAck: true,
    consumer: consumer);

Console.ReadLine();
class Customer
{
    public Customer()
    {
        Id = Guid.NewGuid();
    }
    public Guid Id { get; set; }
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string Email { get; set; } = default!;
}