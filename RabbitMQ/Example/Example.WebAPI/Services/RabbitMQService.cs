using Example.WebAPI.Models;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace Example.WebAPI.Services;

public static class RabbitMQService
{
    private static IModel CreateChannel()
    {
        var factory = new ConnectionFactory() { HostName = "localhost" };
        var connection = factory.CreateConnection();
        var channel = connection.CreateModel();

        return channel;
    }

    public static void CreateDataToElasticSearch(Customer customer)
    {
        Console.WriteLine("Kuyruğa mesajı göndermek için işlemeye başladık");
        var channel = CreateChannel();
        channel.QueueDeclare(
            queue: "elastic",
            durable: true,
            exclusive: false,
            autoDelete: false,
            arguments: null);

        string message = JsonSerializer.Serialize(customer);
        var body = Encoding.UTF8.GetBytes(message);

        channel.BasicPublish(
            exchange: string.Empty,
            routingKey: "elastic",
            basicProperties: null,
            body: body);

        Console.WriteLine("Kuyruğa mesajı gönderdik");
    }

}
