using Bogus;
using eCommerce.MVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace eCommerce.MVC.Controllers;
public class HomeController : Controller
{
    private List<Product> products = new();

    public HomeController()
    {
        Faker faker = new();

        Product product1 = new()
        {
            Name = faker.Commerce.ProductName(),
            ImageUrl = "https://cdn.yemek.com/uploads/2023/06/domates-kac-kalori-shutter-4.jpg",
            Description = faker.Commerce.ProductDescription(),
            Price = faker.Commerce.Price(symbol: "TRY") //altgr+t harfi => türk lirasý sembolü atar
        };

        products.Add(product1);

        faker = new();

        Product product2 = new()
        {
            Name = faker.Commerce.ProductName(),
            Description = faker.Commerce.ProductDescription(),
            Price = faker.Commerce.Price(symbol: "TRY"),
            ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/b/b8/Kiwi_%28Actinidia_chinensis%29_1_Luc_Viatour.jpg/640px-Kiwi_%28Actinidia_chinensis%29_1_Luc_Viatour.jpg"
        };

        products.Add(product2);
    }
    public IActionResult Index() //action - action methodlar
    {
        return View(products);
    }    

    public IActionResult Contact()
    {
        return View();
    }

    public IActionResult ShoppingCart()
    {
        return View();
    }
}
