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
    [ProducesResponseType(typeof(List<TestUser>), StatusCodes.Status200OK, "application/json")]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest, "application/json")]
    public async Task<ActionResult<List<TestUser>>> GetAllTestUsers()
    {
        return await _mediator.Send(new GetAllTestUsersQuery());
    }

    [HttpPost]
    [ProducesResponseType(typeof(int), StatusCodes.Status201Created, "text/plain")]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest, "application/json")]
    public async Task<ActionResult<int>> CreateTestUser([FromBody] CreateTestUserCommand command)
    {
        return await _mediator.Send(command);
    }
}
