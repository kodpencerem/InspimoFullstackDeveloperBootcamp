using eCommerce.Application;
using eCommerce.Application.Products.GetAllProducts;
using eCommerce.Domain.Options;
using eCommerce.Infrastructure;
using MediatR;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplication();
builder.Services.AddInfrastructure();

builder.Services.AddControllers();

builder.Services.AddSwaggerGen();
builder.Services.AddEndpointsApiExplorer();

//builder.Services.Configure<Jwt>(builder.Configuration.GetSection("Jwt")); //options pattern
builder.Services.ConfigureOptions<JwtOptionsSetup>();

builder.Services.AddAuthentication().AddJwtBearer(options =>
{
    var srv = builder.Services.BuildServiceProvider();
    var jwt = srv.GetRequiredService<IOptions<Jwt>>().Value;

    string secretKey = jwt.SecretKey;

    SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(secretKey));

    options.TokenValidationParameters = new()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateIssuerSigningKey = true,
        ValidateLifetime = true,
        ValidIssuer = jwt.Issuer,
        ValidAudience = jwt.Audience,
        IssuerSigningKey = securityKey
    };
});

builder.Services.AddAuthorization();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();


app.UseAuthentication();
app.UseAuthorization();

app.MapGet("/", () => "Hello World!").RequireAuthorization();

app.MapGet("/GetAll", async (IMediator mediator, CancellationToken cancellationToken) =>
{
    GetAllProductsQuery request = new();
    var response = await mediator.Send(request, cancellationToken);

    return Results.Ok(response);
});

app.UseStaticFiles();

app.MapControllers();


app.Run();