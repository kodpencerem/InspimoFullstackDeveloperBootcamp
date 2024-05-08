using Microsoft.AspNetCore.Mvc;

namespace First.WebAPI.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class TestController : ControllerBase
{
    [HttpGet("Saydam")]
    public IActionResult Taner(int? age, string? name, string? address)
    {
        return Ok("Api is working...");
    }

    [HttpGet]
    public IActionResult Hello2(int? age, string? name, string? address)
    {
        return Ok("Api is working...");
    }

    //string int double decimal 

    [HttpPost]
    public IActionResult HelloPost(int? age, string? name, string? address)
    {
        return Ok("Api is working...");
    }



    [HttpPut]
    public IActionResult HelloPut(int? age, string? name, string? address)
    {
        return Ok("Api is working...");
    }

    [HttpDelete]
    public IActionResult HelloDelete(int? age, string? name, string? address)
    {
        return Ok("Api is working...");
    }
}
