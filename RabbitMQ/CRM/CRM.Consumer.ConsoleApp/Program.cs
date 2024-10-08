﻿using FluentEmail.Core;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

ServiceCollection services = new();

services.AddFluentEmail("info@tanersaydam.com").AddSmtpSender("localhost", 25);

var srv = services.BuildServiceProvider();


var factory = new ConnectionFactory
{
    HostName = "localhost"
};
var connection = factory.CreateConnection();
var channel = connection.CreateModel();

channel.ExchangeDeclare(exchange: "emails", type: ExchangeType.Fanout);

var queueName = channel.QueueDeclare().QueueName;

channel.QueueBind(
    queue: queueName,
    exchange: "emails",
    routingKey: string.Empty);

var consumer = new EventingBasicConsumer(channel);
consumer.Received += async (model, ea) =>
{
    var body = ea.Body.ToArray();
    var message = Encoding.UTF8.GetString(body);
    Customer? customer = JsonSerializer.Deserialize<Customer>(message);

    if (customer is not null)
    {
        var fluentEmail = srv.GetRequiredService<IFluentEmail>();
        fluentEmail
        .To(customer.Email)
        .Subject("Bayramınız kutlu olsun")
        .Body("<h1>Bayramınızı en içten dileklerimizle kutlarız</h1>")
        .Send();

        Console.WriteLine($"{customer.Name} adlı müşterinin {customer.Email} mail adresine bayram mesajı başarıyla gönderildi");

        await Task.Delay(500);
    }
};

channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);


Console.ReadLine();




public sealed class Customer
{
    public Customer()
    {
        Id = Guid.NewGuid();
    }
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string Email { get; set; } = default!;
}
