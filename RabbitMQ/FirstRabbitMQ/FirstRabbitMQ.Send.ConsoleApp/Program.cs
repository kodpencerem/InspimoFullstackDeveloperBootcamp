using RabbitMQ.Client;
using System.Text;

//Connection setting
var factory = new ConnectionFactory { HostName = "localhost", Port = 5672, UserName = "admin", Password = "admin" };

//Connection create
var connection = factory.CreateConnection();
Console.WriteLine("Connection is successful");

//Channel Create
var channel = connection.CreateModel();
Console.WriteLine("Create channel is successful");

var arguments = new Dictionary<string, object>()
{
    { "x-message-ttl",60000 }, //kuyruğa eklenen değerin saklanma süresi
    { "x-max-length", 1000 }, //kuyrukta en fazla saklabilecek mesaj sayısı
};

//Queue create
channel.QueueDeclare(
    queue: "hello",
    durable: true, //kuyruğun rabbitmq dursa bile hayatta kalmasını sağlıyor ama kuyruğun içindekini saklamıyor
    exclusive: false,//oluşturduğumuz queue aynı connection tarafından kullanılsın başkası göremesin dersek true veriyoruz. False olursa tüm farklı connectionlar bu queue erişebiliyor
    autoDelete: true,//kuyruğu dinleyen son consumer da kapanırsa kuyruğu yok et
    arguments: arguments);//parametreler
Console.WriteLine("Create queue is successful");

while (true)
{
    //Send message to queue
    string message = "Hello world!";

    var body = Encoding.UTF8.GetBytes(message);

    channel.BasicPublish(
        exchange: string.Empty,
        routingKey: "hello",
        basicProperties: null,
        body: body);
    Console.WriteLine("Queue successfully sent a message");
    Console.ReadLine();
}

