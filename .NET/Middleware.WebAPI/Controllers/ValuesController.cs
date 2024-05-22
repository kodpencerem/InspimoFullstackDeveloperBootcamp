using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Middleware.WebAPI.Filters;

namespace Middleware.WebAPI.Controllers;
[Route("api/[controller]/[action]")]
[ApiController]
public sealed class ValuesController : ControllerBase
{
    [HttpGet("{age}")]
    [MyAuthorize]
    [Authorize]
    public IActionResult Test(string age)
    {
        var context = HttpContext;
        //throw new ArgumentException("Something went wrong");
        return Ok(new { Message = "API is working..." });
    }

    [HttpPost]
    [Log]
    public IActionResult Create(ProductDto request)
    {
        return Ok(request);
    }
}

public sealed record ProductDto(int Id, string Name);