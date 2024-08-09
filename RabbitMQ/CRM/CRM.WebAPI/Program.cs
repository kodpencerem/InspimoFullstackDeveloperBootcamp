using CRM.WebAPI.Models;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");


app.MapGet("/send-all-customers-holiday-message", () =>
{
    List<Customer> customers = new List<Customer>();

    //for (int i = 0; i < 1000; i++)
    //{
    //    Faker faker = new();
    //    Customer customer = new()
    //    {
    //        Name = faker.Person.FullName,
    //        Email = faker.Person.Email,
    //    };
    //    customers.Add(customer);
    //}

    Customer customer1 = new()
    {
        Name = "Taner Saydam",
        Email = "tanersaydam@gmail.com"
    };

    customers.Add(customer1);

    var factory = new ConnectionFactory
    {
        HostName = "localhost",
        UserName = "admin",
        Password = "admin"
    };
    var connection = factory.CreateConnection();
    var channel = connection.CreateModel();

    channel.QueueDeclare(
        queue: "holiday-mail",
        durable: false,
        exclusive: false,
        autoDelete: false,
        arguments: null
        );

    foreach (var customer in customers)
    {
        string message = JsonSerializer.Serialize(customer);
        var body = Encoding.UTF8.GetBytes(message);

        channel.BasicPublish(
            exchange: string.Empty,
            routingKey: "holiday-mail",
            basicProperties: null,
            body: body
            );
    }


    return Results.Ok(new { message = "Tüm müþterilere bayram mesajý sýrayla gönderilmek için kuyruða gönderildi" });

});

app.Run();
