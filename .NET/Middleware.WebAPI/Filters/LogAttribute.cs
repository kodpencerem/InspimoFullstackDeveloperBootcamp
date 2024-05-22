using Microsoft.AspNetCore.Mvc.Filters;

namespace Middleware.WebAPI.Filters;

public sealed class LogAttribute : Attribute, IActionFilter
{
    public void OnActionExecuted(ActionExecutedContext context)
    {
        //metot sonuçlandıktan sonra
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        //metot başlamadan önce
    }
}
