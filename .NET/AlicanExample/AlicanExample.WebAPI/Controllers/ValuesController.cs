using Microsoft.AspNetCore.Mvc;

namespace AlicanExample.WebAPI.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ValuesController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok(new { Message = "API is working..." });
    }
}
