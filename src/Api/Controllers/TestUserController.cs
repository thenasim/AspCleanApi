using Api.Common.Constants;
using Api.Contracts.TestUsers;
using Application.TestUsers.Commands.CreateTestUser;
using Application.TestUsers.Queries.GetAllTestUsers;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

public class TestUserController : ApiControllerBase
{
    private readonly IMapper _mapper;

    public TestUserController(IMapper mapper)
    {
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<TestUserResponse>), StatusCodes.Status200OK, HttpContentTypes.Json)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest, HttpContentTypes.ProblemJson)]
    public async Task<ActionResult<List<TestUserResponse>>> GetAllTestUsers()
    {
        var users = await Mediator.Send(new GetAllTestUsersQuery());
        var response = _mapper.Map<List<TestUserResponse>>(users);
        return response;
    }

    [HttpPost]
    [ProducesResponseType(typeof(int), StatusCodes.Status201Created, HttpContentTypes.TextPlain)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest, HttpContentTypes.ProblemJson)]
    public async Task<ActionResult<int>> CreateTestUser([FromBody] CreateTestUserCommand command)
    {
        return await Mediator.Send(command);
    }
}
