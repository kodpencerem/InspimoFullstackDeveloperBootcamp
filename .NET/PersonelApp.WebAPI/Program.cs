using PersonelApp.WebAPI.Context;
using PersonelApp.WebAPI.Repositories;
using PersonelApp.WebAPI.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddTransient<ApplicationDbContext>();
builder.Services.AddTransient<IPersonelRepository, PersonelRepository>();
builder.Services.AddTransient<IPersonelService, PersonelService>();

var app = builder.Build();

app.Use(async (context, next) =>
{
    try
    {
        await next(context);
    }
    catch (Exception ex)
    {
        context.Response.StatusCode = 500;
        await context.Response.WriteAsync(ex.Message);
    }
});

app.MapControllers();


app.Run();
