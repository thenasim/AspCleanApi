using Api.Contracts.TestUsers;
using Application.TestUsers.Commands.CreateTestUser;
using Application.TestUsers.Queries.GetAllTestUsers;
using Domain.Entities;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

public class TestUsersController : ApiControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public TestUsersController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<TestUserResponse>), StatusCodes.Status200OK, "application/json")]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest, "application/json")]
    public async Task<ActionResult<List<TestUserResponse>>> GetAllTestUsers()
    {
        var users = await _mediator.Send(new GetAllTestUsersQuery());
        var response = _mapper.Map<List<TestUserResponse>>(users);
        return response;
    }

    [HttpPost]
    [ProducesResponseType(typeof(int), StatusCodes.Status201Created, "text/plain")]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest, "application/json")]
    public async Task<ActionResult<int>> CreateTestUser([FromBody] CreateTestUserCommand command)
    {
        return await _mediator.Send(command);
    }
}
