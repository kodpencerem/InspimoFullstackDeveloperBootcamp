using eCommerce.Application.Products.CreateProduct;
using eCommerce.Application.Products.GetAllProducts;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eCommerce.WebAPI.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ProductsController(
    IMediator mediator) : ControllerBase
{
    [HttpPost]
    [Authorize(Roles = "Products.Create")]
    public async Task<IActionResult> Create([FromForm] CreateProductCommand request, CancellationToken cancellationToken)
    {
        var response = await mediator.Send(request, cancellationToken);

        return Ok(response);
    }

    [HttpGet]
    [Authorize(Roles = "Products.GetAll")]
    public async Task<IActionResult> GetAll([FromForm] GetAllProductsQuery request, CancellationToken cancellationToken)
    {
        var response = await mediator.Send(request, cancellationToken);

        return Ok(response);
    }
}
