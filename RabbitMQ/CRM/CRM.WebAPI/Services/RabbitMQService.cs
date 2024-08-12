using CRM.WebAPI.Models;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace CRM.WebAPI.Services;

public static class RabbitMQService
{
    private static IModel CreateChannel()
    {
        var factory = new ConnectionFactory
        {
            HostName = "localhost"
        };
        var connection = factory.CreateConnection();
        var channel = connection.CreateModel();

        return channel;
    }

    public static void SendHolidayMessageHelloWorld(List<Customer> customers)
    {
        var channel = CreateChannel();
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
    }

    public static void SendHolidayMessageWorkQueues(List<Customer> customers)
    {
        var channel = CreateChannel();
        channel.QueueDeclare(
        queue: "holiday-mail",
        durable: true,//bu kuyruğu saklar
        exclusive: false,
        autoDelete: false,
        arguments: null
        );

        var properties = channel.CreateBasicProperties();
        properties.Persistent = true; //kuyrukla beraber içindekileri de sakla

        foreach (var customer in customers)
        {
            string message = JsonSerializer.Serialize(customer);
            var body = Encoding.UTF8.GetBytes(message);

            channel.BasicPublish(
                exchange: string.Empty,
                routingKey: "holiday-mail",
                basicProperties: properties,
                body: body
                );
        }
    }

    public static void SendHolidayMessagePublishSubscribe(List<Customer> customers)
    {
        var channel = CreateChannel();

        channel.ExchangeDeclare(exchange: "emails", type: ExchangeType.Fanout);

        foreach (var customer in customers)
        {
            string message = JsonSerializer.Serialize(customer);
            var body = Encoding.UTF8.GetBytes(message);

            channel.BasicPublish(
                exchange: "emails",
                routingKey: string.Empty,
                basicProperties: null,
                body: body
                );
        }
    }
}
