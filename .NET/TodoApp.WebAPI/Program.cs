using TodoApp.WebAPI.Context;
using TodoApp.WebAPI.Services;

var builder = WebApplication.CreateBuilder(args);

//Dependency Injection
builder.Services.AddTransient<ApplicationDbContext>();
builder.Services.AddTransient<ITodoService, TodoService>();

builder.Services.AddControllers();


var app = builder.Build();


app.MapControllers();

app.Run();
