using MinimalAPI.Endpoints;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();

var app = builder.Build();

app.UseHttpsRedirection();

app.MapControllers();

app.AddTestEnpoints();

app.Run();