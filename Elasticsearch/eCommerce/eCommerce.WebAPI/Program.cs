using eCommerce.Application;
using eCommerce.Application.Products.GetAllProducts;
using eCommerce.Infrastructure;
using MediatR;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplication();
builder.Services.AddInfrastructure();

builder.Services.AddControllers();

builder.Services.AddSwaggerGen();
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapGet("/", () => "Hello World!");

app.MapGet("/GetAll", async (IMediator mediator, CancellationToken cancellationToken) =>
{
    GetAllProductsQuery request = new();
    var response = await mediator.Send(request, cancellationToken);

    return Results.Ok(response);
});

app.UseStaticFiles();

app.MapControllers();


app.Run();
