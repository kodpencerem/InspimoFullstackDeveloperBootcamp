using Microsoft.AspNetCore.Mvc;

namespace Middle.WebAPI.Controllers;
[Route("api/[controller]/[action]")]
[ApiController]
public class ValuesController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        try
        {
            int a = 0;
            int b = 0;
            int c = a / b; //DivideByZero Exception
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }

        return Ok();
    }
}
