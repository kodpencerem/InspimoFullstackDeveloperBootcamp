using Microsoft.AspNetCore.Mvc;

namespace DependencyInjection.New.WebAPI.Controllers;
[Route("api/[controller]/[action]")]
[ApiController]
public sealed class ProductsController(IProductService productService) : ControllerBase
{
    //ProductService _productService;
    //public ProductsController(ProductService productService)
    //{
    //    _productService = productService;
    //}

    [HttpPost]
    public IActionResult Create(Product product)
    {
        // ProductService productService = new ProductService();
        productService.Create(product);

        return Created();
    }
}
public record Product(int Id, string Name);

public interface IProductService
{
    void Create(Product product);
}

public class ProductService : IProductService
{
    public void Create(Product product)
    {
        //MSSQL db ye kayıt et        
    }
}

public class NewProductService : IProductService
{
    public void Create(Product product)
    {
        //Postgre db ye kayıt et
    }
}
