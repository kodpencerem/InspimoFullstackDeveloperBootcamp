using Microsoft.AspNetCore.Diagnostics;
using System.Net.Mime;
using System.Text.Json;

namespace eCommerce.WebAPI.Middlewares;

public sealed class ExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext context, Exception exception, CancellationToken cancellationToken)
    {
        context.Response.ContentType = MediaTypeNames.Application.Json;
        if (exception.GetType() == typeof(UnauthorizedAccessException))
        {
            context.Response.StatusCode = 403;
        }

        object error = new { Message = exception.Message };
        string errorString = JsonSerializer.Serialize(error);

        await context.Response.WriteAsync(errorString);

        return true;
    }
}