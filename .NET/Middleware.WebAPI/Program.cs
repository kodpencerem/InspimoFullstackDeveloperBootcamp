using Middleware.WebAPI.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors();

builder.Services.AddControllers();
//service registration // Dependency Injection

builder.Services.AddTransient<ExampleMiddleware>();

var app = builder.Build();

//middleware

app.UseCors(x => x.AllowAnyOrigin());

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.UseMiddleware<ExampleMiddleware>();

app.Use((context, next) =>
{
    return next(context);
});

//app.Use(async (context, next) =>
//{
//    //context.Request.QueryString = QueryString.Create("age", "10");
//    //context.Response.StatusCode = 501;
//    try
//    {
//        await next(context);
//    }
//    catch (Exception ex)
//    {
//        context.Response.StatusCode = 500;
//        await context.Response.WriteAsync(ex.StackTrace!);
//    }
//});

app.Run();