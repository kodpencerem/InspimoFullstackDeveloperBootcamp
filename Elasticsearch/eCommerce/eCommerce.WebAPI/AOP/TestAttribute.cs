using Microsoft.AspNetCore.Mvc.Filters;

namespace eCommerce.WebAPI.AOP;

public class TestAttribute : Attribute, IActionFilter
{
    public void OnActionExecuted(ActionExecutedContext context)
    {
        //metot bitip son kullanıcıya dönerken çalışır
        //geriye dönderilen response'u yakalayabiliyoruz
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        //metot henüz başlamadan önce çalıştırılır
        //burada parametre olarak istenen body veya params değerlerini alabiliriz
        //params değerleri => query params beya route params
    }
}
