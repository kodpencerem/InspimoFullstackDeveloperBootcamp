using PersonelApp.WebAPI.Context;
using PersonelApp.WebAPI.Filters;
using PersonelApp.WebAPI.Repositories;
using PersonelApp.WebAPI.Services;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.MSSqlServer;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors();

builder.Services.AddControllers();

builder.Services.AddTransient<ApplicationDbContext>();
builder.Services.AddTransient<IPersonelRepository, PersonelRepository>();
builder.Services.AddTransient<IPersonelService, PersonelService>();

builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<IUserService, UserService>();

builder.Services.AddTransient<IAuthTokenRepository, AuthTokenRepository>();
builder.Services.AddTransient<IAuthTokenService, AuthTokenService>();

builder.Services.AddMemoryCache();

builder.Services.AddSwaggerGen();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddAutoMapper(typeof(Program).Assembly);

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console(LogEventLevel.Information)
    .WriteTo.File("./log.txt", LogEventLevel.Information, rollingInterval: RollingInterval.Month)
    .WriteTo.MSSqlServer(
    connectionString: builder.Configuration.GetConnectionString("SqlServer"),
    sinkOptions: new MSSqlServerSinkOptions()
    {
        TableName = "Logs",
        AutoCreateSqlTable = true
    })
    .CreateLogger();

builder.Host.UseSerilog();

builder.Services.AddExceptionHandler<MyExceptionHandler>().AddProblemDetails();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseCors(x => x.AllowAnyOrigin());

app.MapControllers();

app.UseExceptionHandler();
//app.UseMiddleware<MyExceptionHandler>();

app.Run();
