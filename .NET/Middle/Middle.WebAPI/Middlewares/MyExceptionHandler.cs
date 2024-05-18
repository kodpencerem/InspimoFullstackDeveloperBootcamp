using Microsoft.AspNetCore.Diagnostics;
using Middle.WebAPI.Exceptions;
using System.Net.Mime;
using System.Text.Json;

namespace Middle.WebAPI.Middlewares;

public sealed class MyExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext context, Exception exception, CancellationToken cancellationToken)
    {
        context.Response.StatusCode = 500;
        context.Response.ContentType = MediaTypeNames.Application.Json;


        if (exception.GetType() == typeof(WeCannotFindYourUserException))
        {
            context.Response.StatusCode = 501;
        }

        //var obj = new { Message = exception.Message };
        //var text = JsonSerializer.Serialize(obj);

        var text = new ErrorResult(exception.Message).ToString();

        await context.Response.WriteAsync(text);

        return true; //21:15 görüşelim
    }
}

public sealed record ErrorResult(string Message)
{
    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}