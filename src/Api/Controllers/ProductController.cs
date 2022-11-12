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
    public async Task<IActionResult> Lists([FromQuery] GetProductsWithPaginationQuery query)
    {
        var result = await Mediator.Send(query);

        return result.Match(
          value => Ok(value),
          errors => Problem(errors)
        );
    }

    [HttpPost]
    [ProducesResponseType(typeof(int), StatusCodes.Status201Created, HttpContentTypes.TextPlain)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest, HttpContentTypes.ProblemJson)]
    public async Task<IActionResult> Create([FromBody] CreateProductCommand command)
    {
        var result = await Mediator.Send(command);

        return result.Match(
          value =>
          {
              Response.StatusCode = StatusCodes.Status201Created;
              return Ok(value);
          },
          errors => Problem(errors)
        );
    }
}
