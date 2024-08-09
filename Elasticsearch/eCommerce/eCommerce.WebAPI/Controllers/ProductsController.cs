using eCommerce.Application.Products.CreateProduct;
using eCommerce.Application.Products.GetAllProducts;
using eCommerce.Application.Products.SeedDataProduct;
using eCommerce.WebAPI.AOP;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eCommerce.WebAPI.Controllers;
[Route("api/[controller]/[action]")]
[ApiController]
[Authorize]
public class ProductsController(
    IMediator mediator) : ControllerBase
{
    [HttpPost]
    [Role("Products.Create")]
    public async Task<IActionResult> Create([FromForm] CreateProductCommand request, CancellationToken cancellationToken)
    {
        var response = await mediator.Send(request, cancellationToken);

        return Ok(response);
    }

    [HttpGet]
    [Role("Products.GetAll")]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        GetAllProductsQuery request = new();
        var response = await mediator.Send(request, cancellationToken);
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> SeedData(CancellationToken cancellationToken)
    {
        SeedDataProductCommand request = new();
        var response = await mediator.Send(request, cancellationToken);

        return Ok(new { Message = response });
    }
}
