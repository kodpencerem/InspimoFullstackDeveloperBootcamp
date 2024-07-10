using eOkulServer.Domain.Abstracts;
using Microsoft.AspNetCore.Diagnostics;
using System.Text.Json;

namespace eOkulServer.WebAPI.Middlewares;

public sealed class MyExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        httpContext.Response.StatusCode = 500;
        httpContext.Response.ContentType = "application/json";
        object error = Result<string>.Failure(exception.Message, 500);
        string errorString = JsonSerializer.Serialize(error);
        await httpContext.Response.WriteAsync(errorString);

        return true;
    }
}
