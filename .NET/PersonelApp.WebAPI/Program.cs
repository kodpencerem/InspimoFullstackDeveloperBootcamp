using PersonelApp.WebAPI.Context;
using PersonelApp.WebAPI.Filters;
using PersonelApp.WebAPI.Repositories;
using PersonelApp.WebAPI.Services;

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


//builder.Services.AddTransient<MyExceptionMiddleware>();

builder.Services.AddExceptionHandler<MyExceptionHandler>().AddProblemDetails();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseCors(x => x.AllowAnyOrigin());

app.MapControllers();

app.UseExceptionHandler();
//app.UseMiddleware<MyExceptionHandler>();

app.Run();
