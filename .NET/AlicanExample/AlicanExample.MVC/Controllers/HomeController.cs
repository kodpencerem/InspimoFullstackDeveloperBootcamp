using AlicanExample.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AlicanExample.MVC.Controllers;
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public async Task<IActionResult> Index()
    {
        HttpClient httpClient = new();
        HttpResponseMessage message = await httpClient.GetAsync("https://localhost:7158/api/Values");
        if(message.IsSuccessStatusCode)
        {
            object response = await message.Content.ReadFromJsonAsync<object>();

            return View(response);
        }

        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
