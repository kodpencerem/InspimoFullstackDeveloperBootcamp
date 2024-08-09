using eCommerce.Application;
using eCommerce.Application.Products.GetAllProducts;
using eCommerce.Domain.Options;
using eCommerce.Infrastructure;
using eCommerce.WebAPI.Middlewares;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Sinks.Elasticsearch;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplication();
builder.Services.AddInfrastructure();

builder.Services.AddControllers();

builder.Services.AddSwaggerGen(setup =>
{
    var jwtSecuritySheme = new OpenApiSecurityScheme
    {
        BearerFormat = "JWT",
        Name = "JWT Authentication",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = JwtBearerDefaults.AuthenticationScheme,
        Description = "Put **_ONLY_** yourt JWT Bearer token on textbox below!",

        Reference = new OpenApiReference
        {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme
        }
    };

    setup.AddSecurityDefinition(jwtSecuritySheme.Reference.Id, jwtSecuritySheme);

    setup.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    { jwtSecuritySheme, Array.Empty<string>() }
                });
});
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddMemoryCache();

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

builder.Services.AddExceptionHandler<ExceptionHandler>().AddProblemDetails();


Log.Logger = new LoggerConfiguration()
    .WriteTo.Console(Serilog.Events.LogEventLevel.Information)
    //.WriteTo.File("./log.txt", Serilog.Events.LogEventLevel.Information, rollingInterval: RollingInterval.Minute)
    .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri("http://localhost:9200"))
    {
        MinimumLogEventLevel = Serilog.Events.LogEventLevel.Error,
        AutoRegisterTemplate = true,
        IndexFormat = "logstash-{0:yyyy.MM.dd}",
        FailureCallback = (e, ex) => Console.WriteLine("Unable to submit event " + e.MessageTemplate),
        EmitEventFailure = EmitEventFailureHandling.WriteToSelfLog |
                           EmitEventFailureHandling.RaiseCallback |
                           EmitEventFailureHandling.ThrowException

    })
    .CreateLogger();


builder.Host.UseSerilog();

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

app.UseExceptionHandler();

app.MapControllers();


app.Run();