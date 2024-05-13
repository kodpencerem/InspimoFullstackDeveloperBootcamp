namespace HttpContext.WebAPI.Services;

public class UserService
{
    public void Create()
    {
        HttpContextAccessor httpContextAccessor = new();

        var httpContext = httpContextAccessor.HttpContext;
    }
}
