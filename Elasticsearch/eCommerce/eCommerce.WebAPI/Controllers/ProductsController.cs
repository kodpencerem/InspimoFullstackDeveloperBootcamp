using eCommerce.Application.Products.CreateProduct;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace eCommerce.WebAPI.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ProductsController(
    IMediator mediator) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create([FromForm] CreateProductCommand request, CancellationToken cancellationToken)
    {
        var response = await mediator.Send(request, cancellationToken);

        return Ok(response);
    }
}
