using CRM.WebAPI.Models;
using CRM.WebAPI.Services;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");


app.MapGet("/send-all-customers-holiday-message", () =>
{
    List<Customer> customers = new List<Customer>();

    Customer customer1 = new()
    {
        Name = "Taner Saydam",
        Email = "tanersaydam@gmail.com"
    };

    Customer customer2 = new()
    {
        Name = "Toprak Saydam",
        Email = "topraksaydam@gmail.com"
    };

    Customer customer3 = new()
    {
        Name = "Tahir Saydam",
        Email = "tahirsaydam@gmail.com"
    };



    customers.AddRange(new List<Customer>() { customer1, customer2, customer3 });

    RabbitMQService.SendHolidayMessagePublishSubscribe(customers);

    return Results.Ok(new { message = "Tüm müþterilere bayram mesajý sýrayla gönderilmek için kuyruða gönderildi" });

});

app.Run();
