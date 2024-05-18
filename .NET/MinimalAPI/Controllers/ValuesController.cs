using Microsoft.AspNetCore.Mvc;

namespace MinimalAPI.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ValuesController : ControllerBase
{
    [HttpGet]
    //[Authorize]
    public IActionResult Get()
    {
        return Ok("This is controller response");
    }
}