using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace eCommerce.WebAPI.AOP;

public class RoleAttribute(
    string role) : Attribute, IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        Claim? claim = context.HttpContext.User.Claims.FirstOrDefault(p => p.Type == ClaimTypes.Role);

        if (claim is null || !claim.Value.Contains(role))
        {
            throw new UnauthorizedAccessException();
        }
    }
}
