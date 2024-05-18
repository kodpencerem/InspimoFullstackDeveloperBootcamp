using System.Net.Mime;
using System.Text.Json;

namespace Middle.WebAPI.Middlewares;

public sealed class ExceptionMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            context.Response.StatusCode = 500;
            context.Response.ContentType = MediaTypeNames.Application.Json;
            var obj = new { Message = ex.Message };
            string text = JsonSerializer.Serialize(obj);
            await context.Response.WriteAsync(text);
        }
    }
}
