using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TSChat.WebAPI.Context;
using TSChat.WebAPI.Hubs;
using TSChat.WebAPI.Options;
using TSChat.WebAPI.Repositories;
using TSChat.WebAPI.Services;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("PostgreSQL"));
    options.UseSnakeCaseNamingConvention();
});

builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("Jwt"));

builder.Services.AddHttpContextAccessor();

builder.Services.AddSignalR();

ServiceProvider srv = builder.Services.BuildServiceProvider();

IOptions<JwtOptions> jwtOptions = srv.GetRequiredService<IOptions<JwtOptions>>();

builder.Services.AddAuthentication().AddJwtBearer(options =>
{
    options.TokenValidationParameters = new()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtOptions.Value.Issuer,
        ValidAudience = jwtOptions.Value.Audience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Value.SecretKey))
    };
});

builder.Services.AddAuthorization(configure =>
{
    configure.DefaultPolicy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .AddAuthenticationSchemes("Bearer")
        .Build();
});

builder.Services.AddCors();

builder.Services.AddControllers();

builder.Services.AddTransient<AuthService>();
builder.Services.AddTransient<JwtProvider>();
builder.Services.AddTransient<UserService>();
builder.Services.AddTransient<ChatService>();
builder.Services.AddTransient<IUserRepository, UserEFCoreRepository>();
builder.Services.AddTransient<IChatRepository, ChatEFCoreRepository>();

builder.Services.AddSwaggerGen();
builder.Services.AddEndpointsApiExplorer();

WebApplication app = builder.Build();

app.UseSwagger();

app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseCors(x => x.AllowCredentials().AllowAnyHeader().AllowAnyMethod().SetIsOriginAllowed(x => true));

app.UseStaticFiles();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers().RequireAuthorization();

app.MapHub<ChatHub>("/chat");

app.Run();
