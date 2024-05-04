using Bogus;
using eCommerce.MVC.Context;
using eCommerce.MVC.DTOs;
using eCommerce.MVC.Models;
using Iyzipay;
using Iyzipay.Model;
using Iyzipay.Request;
using Microsoft.AspNetCore.Mvc;

namespace eCommerce.MVC.Controllers;
public class HomeController : Controller
{
    private readonly ApplicationDbContext context = new();
    public IActionResult Index() //action - action methodlar
    {
        var products = context.Products.ToList();
        return View(products);
    }    

    public IActionResult Contact()
    {
        return View();
    }
   
    public IActionResult ShoppingCart()
    {
        var shoppingCarts = context.ShoppingCarts.ToList();

        decimal total = 0;
        foreach (var item in shoppingCarts)
        {
            total += item.Price;
        }

        ViewBag.Total = total;
        //TempData["Total"] = total;

        return View(shoppingCarts);
    }

    [HttpGet]
    public IActionResult AddShoppingCart(int id)
    {
        Product product = context.Products.First(x=> x.Id == id);

        ShoppingCart shoppingCart = new()
        {
            Name = product.Name,
            ImageUrl = product.ImageUrl,
            Price = product.Price
        };

        context.Add(shoppingCart);
        context.SaveChanges();

        return RedirectToAction("Index","Home");
    }

    [HttpPost]
    public IActionResult Pay(PayDto payDto)
    {
        var shoppingCarts = context.ShoppingCarts.ToList();

        decimal total = 0;
        foreach (var item in shoppingCarts)
        {
            total += item.Price;
        }

        Options options = new Options();
        options.ApiKey = "sandbox-pLQ1N137xokDIfkHtu2GfwectFMAH4DB";
        options.SecretKey = "sandbox-RKZ6gAX8GzqbHEyyy0AcPjgCs3ANsOKO";
        options.BaseUrl = "https://sandbox-api.iyzipay.com";

        CreatePaymentRequest request = new CreatePaymentRequest();
        request.Locale = Locale.TR.ToString();
        request.ConversationId = Guid.NewGuid().ToString();
        request.Price = total.ToString().Replace(",",".");
        request.PaidPrice = total.ToString().Replace(",", ".");
        request.Currency = Currency.TRY.ToString();
        request.Installment = 1;
        request.BasketId = Guid.NewGuid().ToString();
        request.PaymentChannel = PaymentChannel.WEB.ToString();
        request.PaymentGroup = PaymentGroup.PRODUCT.ToString();

        PaymentCard paymentCard = new PaymentCard();
        paymentCard.CardHolderName = payDto.Owner;
        paymentCard.CardNumber = payDto.CardNumber;
        paymentCard.ExpireMonth = payDto.ExpiryDate.Split("/")[0];
        paymentCard.ExpireYear = payDto.ExpiryDate.Split("/")[1];
        paymentCard.Cvc = payDto.CVC;
        paymentCard.RegisterCard = 0;
        request.PaymentCard = paymentCard;

        Buyer buyer = new Buyer();
        buyer.Id = "BY789";
        buyer.Name = "John";
        buyer.Surname = "Doe";
        buyer.GsmNumber = "+905350000000";
        buyer.Email = "email@email.com";
        buyer.IdentityNumber = "74300864791";
        buyer.LastLoginDate = "2015-10-05 12:43:35";
        buyer.RegistrationDate = "2013-04-21 15:12:09";
        buyer.RegistrationAddress = "Nidakule Göztepe, Merdivenköy Mah. Bora Sok. No:1";
        buyer.Ip = "85.34.78.112";
        buyer.City = "Istanbul";
        buyer.Country = "Turkey";
        buyer.ZipCode = "34732";
        request.Buyer = buyer;

        Address shippingAddress = new Address();
        shippingAddress.ContactName = "Jane Doe";
        shippingAddress.City = "Istanbul";
        shippingAddress.Country = "Turkey";
        shippingAddress.Description = "Nidakule Göztepe, Merdivenköy Mah. Bora Sok. No:1";
        shippingAddress.ZipCode = "34742";
        request.ShippingAddress = shippingAddress;

        Address billingAddress = new Address();
        billingAddress.ContactName = "Jane Doe";
        billingAddress.City = "Istanbul";
        billingAddress.Country = "Turkey";
        billingAddress.Description = "Nidakule Göztepe, Merdivenköy Mah. Bora Sok. No:1";
        billingAddress.ZipCode = "34742";
        request.BillingAddress = billingAddress;

        List<BasketItem> basketItems = new List<BasketItem>();
        foreach (var card in shoppingCarts)
        {
            BasketItem basketItem = new BasketItem();
            basketItem.Id = Guid.NewGuid().ToString();
            basketItem.Name = card.Name;
            basketItem.ItemType = BasketItemType.PHYSICAL.ToString();
            basketItem.Price = card.Price.ToString().Replace(",", ".");
            basketItem.Category1 = "TS Ürün";
            basketItems.Add(basketItem);
        }
        request.BasketItems = basketItems;

        Payment payment = Payment.Create(request, options);
        

        if(payment.Status == "success")
        {
            context.RemoveRange(shoppingCarts);
            context.SaveChanges();
            ViewBag.Total = 0;
            return RedirectToAction("ShoppingCart");
        }

        TempData["Error"] = payment.ErrorMessage;

        return RedirectToAction("ShoppingCart");
    }

    public IActionResult Products()
    {
        var products = context.Products.ToList();
        return View(products);
    }

    [HttpPost]
    public IActionResult CreateProduct(CreateProductDto request)
    {
        string format = request.File!.FileName.Substring(request.File!.FileName.LastIndexOf("."));
        string fileName = Guid.NewGuid().ToString() + format;
        using (var stream = System.IO.File.Create("wwwroot/images/" + request.File!.FileName))
        {
            request.File.CopyTo(stream);
        }

        Product product = new()
        {
            Name = request.Name,
            Description = request.Description,
            ImageUrl = request.File.FileName,
            Price = request.Price
        };

        context.Add(product);
        context.SaveChanges();

        return RedirectToAction("Products");
    }
}
