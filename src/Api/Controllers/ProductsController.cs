using Application.Products.Commands.CreateProduct;
using Application.Products.Queries.GetProductsWithPagination;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

public class ProductsController : ApiControllerBase
{
    private readonly IMediator _mediator;

    public ProductsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<List<Product>>> Lists([FromQuery] GetProductsWithPaginationQuery query)
    {
        return await _mediator.Send(query);
    }

    [HttpPost]
    public async Task<ActionResult<int>> Create([FromBody] CreateProductCommand command)
    {
        return await _mediator.Send(command);
    }
}
