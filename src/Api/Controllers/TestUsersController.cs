using Application.TestUsers.Commands.CreateTestUser;
using Application.TestUsers.Queries.GetAllTestUsers;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

public class TestUsersController : ApiControllerBase
{
    private readonly IMediator _mediator;

    public TestUsersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<List<TestUser>>> GetAllTestUsers([FromQuery] GetAllTestUsersQuery query)
    {
        return await _mediator.Send(query);
    }

    [HttpPost]
    public async Task<ActionResult<int>> CreateTestUser([FromBody] CreateTestUserCommand command)
    {
        return await _mediator.Send(command);
    }
}
