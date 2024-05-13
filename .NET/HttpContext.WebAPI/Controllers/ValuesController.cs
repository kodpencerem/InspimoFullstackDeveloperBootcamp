using Microsoft.AspNetCore.Mvc;

namespace HttpContext.WebAPI.Controllers;
[Route("api/[controller]/[action]")]
[ApiController]
public sealed class ValuesController : ControllerBase
{
    [HttpGet("{age}/{name}")]
    public IActionResult Get(int age, string name) //route params
    {
        var httpContext = HttpContext;

        return NoContent();
    }
}
