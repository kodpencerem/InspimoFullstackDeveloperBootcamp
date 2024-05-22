using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Middleware.WebAPI.Filters;

public sealed class MyAuthorize : Attribute, IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        string? secretkey = context.HttpContext.Request.Headers.FirstOrDefault(p => p.Key == "SecretKey").Value;

        if (string.IsNullOrEmpty(secretkey) && secretkey != "My Secret")
        {
            context.Result = new UnauthorizedResult();
        }

    }
}
