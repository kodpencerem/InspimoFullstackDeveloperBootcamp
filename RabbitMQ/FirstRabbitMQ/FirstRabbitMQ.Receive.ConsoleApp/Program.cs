using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

var factory = new ConnectionFactory
{
    HostName = "localhost",
    Port = 5672,
    UserName = "admin",
    Password = "admin"
};
var connection = factory.CreateConnection();
var channel = connection.CreateModel();

//eğer kuyruğu dinlemeye başladığımda henüz kuyruk oluşmamış ise o zaman kuyruğu sen oluştur
channel.QueueDeclare(
    queue: "hello",
    durable: true,
    exclusive: false,
    autoDelete: true,
    arguments: null);

var consumer = new EventingBasicConsumer(channel);
consumer.Received += (model, ea) =>
{
    var body = ea.Body.ToArray();
    var message = Encoding.UTF8.GetString(body);
    Console.WriteLine($" [x] Received {message}");
};
channel.BasicConsume(
    queue: "hello",
    autoAck: true,
    consumer: consumer);

Console.WriteLine("Queue is listening... Press enter to exit.");

Console.ReadLine();