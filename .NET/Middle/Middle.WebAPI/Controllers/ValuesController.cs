using Microsoft.AspNetCore.Mvc;
using Middle.WebAPI.Exceptions;

namespace Middle.WebAPI.Controllers;
[Route("api/[controller]/[action]")]
[ApiController]
public class ValuesController : ControllerBase
{
    [HttpGet]
    //[EnableRateLimiting("fixed")]
    //[DisableRateLimiting]
    public IActionResult Get()
    {
        return Ok(new { Message = "API is working..." });
    }

    [HttpGet]
    public IActionResult GetException()
    {
        throw new WeCannotFindYourUserException();
    }
}
