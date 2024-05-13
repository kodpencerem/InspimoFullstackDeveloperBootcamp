using HttpContext.WebAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace HttpContext.WebAPI.Controllers;
[Route("api/[controller]/[action]")]
[ApiController]
public sealed class ValuesController : ControllerBase
{
    [HttpGet("{age}/{name}")]
    public async Task<IActionResult> Get(int age, string name) //route params
    {
        var httpContext = HttpContext;
        httpContext.Response.StatusCode = 200;
        await httpContext.Response.WriteAsync("API is working...");
        await httpContext.Response.CompleteAsync();



        return NoContent();
    }


    [HttpPost]
    public IActionResult Create(UserDto request)
    {
        //var httpContext = HttpContext;

        UserService userService = new();
        userService.Create();
        return Created();
    }
}

public record UserDto(string Email, string Password);
