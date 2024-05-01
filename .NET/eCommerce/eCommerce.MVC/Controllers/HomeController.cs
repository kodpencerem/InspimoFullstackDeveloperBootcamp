using Microsoft.AspNetCore.Mvc;

namespace eCommerce.MVC.Controllers;
public class HomeController : Controller
{
    public IActionResult Index() //action - action methodlar
    {
        return View();
    }    

    public IActionResult Contact()
    {
        return View();
    }
}
