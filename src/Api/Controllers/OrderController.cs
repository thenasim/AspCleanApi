using Api.Common.Constants;
using Application.Orders.Commands.CreateOrder;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

public class OrderController : ApiControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(int), StatusCodes.Status201Created, HttpContentTypes.TextPlain)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest, HttpContentTypes.ProblemJson)]
    public async Task<ActionResult<int>> CreateOrder([FromBody] CreateOrderCommand command)
    {
        return await Mediator.Send(command);
    }
}
