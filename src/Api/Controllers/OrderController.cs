using Application.Orders.Commands;
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

    public async Task<ActionResult<int>> CreateOrder([FromQuery] CreateOrderCommand command)
    {
        return await _mediatr.Send(command);
    }
}
