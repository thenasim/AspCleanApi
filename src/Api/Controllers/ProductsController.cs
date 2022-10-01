using Api.Contracts.Products;
using Application.Products.Commands.CreateProduct;
using Application.Products.Queries.GetProductsWithPagination;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

public class ProductsController : ApiControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public ProductsController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<ProductResponse>), StatusCodes.Status200OK, "application/json")]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest, "application/json")]
    public async Task<ActionResult<List<ProductResponse>>> Lists([FromQuery] GetProductsWithPaginationQuery query)
    {
        var products = await _mediator.Send(query);
        var response = _mapper.Map<List<ProductResponse>>(products);
        return response;
    }

    [HttpPost]
    [ProducesResponseType(typeof(int), StatusCodes.Status201Created, "text/plain")]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest, "application/json")]
    public async Task<ActionResult<int>> Create([FromBody] CreateProductCommand command)
    {
        Response.StatusCode = StatusCodes.Status201Created;
        return await _mediator.Send(command);
    }
}
