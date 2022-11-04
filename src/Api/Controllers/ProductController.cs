using Api.Common.Constants;
using Application.Products.Commands.CreateProduct;
using Application.Products.Queries.GetProductsWithPagination;
using Application.Products.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

public class ProductController : ApiControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(List<ProductResponse>), StatusCodes.Status200OK, HttpContentTypes.Json)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest, HttpContentTypes.ProblemJson)]
    public async Task<ActionResult<List<ProductResponse>>> Lists([FromQuery] GetProductsWithPaginationQuery query)
    {
        return await Mediator.Send(query);
    }

    [HttpPost]
    [ProducesResponseType(typeof(int), StatusCodes.Status201Created, HttpContentTypes.TextPlain)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest, HttpContentTypes.ProblemJson)]
    public async Task<ActionResult<int>> Create([FromBody] CreateProductCommand command)
    {
        Response.StatusCode = StatusCodes.Status201Created;
        return await Mediator.Send(command);
    }
}
