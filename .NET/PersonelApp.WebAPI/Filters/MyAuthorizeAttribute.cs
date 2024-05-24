using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Primitives;

namespace PersonelApp.WebAPI.Filters;

public sealed class MyAuthorizeAttribute : Attribute, IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        KeyValuePair<string, StringValues> secretKeyHeader =
            context.HttpContext.Request.Headers.FirstOrDefault(p => p.Key == "SecretKey");

        if (secretKeyHeader.Key is null || secretKeyHeader.Value != "My Secret Key")
        {
            context.Result = new UnauthorizedResult();
        }
    }
}