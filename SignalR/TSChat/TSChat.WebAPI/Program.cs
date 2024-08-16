using TSChat.WebAPI.Repositories;
using TSChat.WebAPI.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors();

builder.Services.AddControllers();

builder.Services.AddTransient<AuthService>();
builder.Services.AddTransient<IUserRepository, UserElasticSearchRepository>();

builder.Services.AddSwaggerGen();
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseCors(x => x.AllowCredentials().AllowAnyHeader().AllowAnyMethod().SetIsOriginAllowed(x => true));

app.UseStaticFiles();

app.MapControllers();

app.Run();
