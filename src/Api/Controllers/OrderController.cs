using Application.Orders.Commands.CreateOrder;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

public class OrderController : ApiControllerBase
{
    private readonly IMediator _mediatr;

    public OrderController(IMediator mediatr)
    {
        _mediatr = mediatr;
    }

    [HttpPost]
    [ProducesResponseType(typeof(int), StatusCodes.Status201Created, "text/plain")]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest, "application/json")]
    public async Task<ActionResult<int>> CreateOrder([FromBody] CreateOrderCommand command)
    {
        return await _mediatr.Send(command);
    }
}
