using Api.Common.Constants;
using Application.TestUsers.Commands.CreateTestUser;
using Application.TestUsers.Queries.CheckUsernameAvailable;
using Application.TestUsers.Queries.GetAllTestUsers;
using Application.TestUsers.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

public class TestUserController : ApiControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(List<TestUserResponse>), StatusCodes.Status200OK, HttpContentTypes.Json)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest, HttpContentTypes.ProblemJson)]
    public async Task<ActionResult<List<TestUserResponse>>> GetAllTestUsers()
    {
        return await Mediator.Send(new GetAllTestUsersQuery());
    }

    [HttpPost]
    [ProducesResponseType(typeof(int), StatusCodes.Status201Created, HttpContentTypes.TextPlain)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest, HttpContentTypes.ProblemJson)]
    public async Task<ActionResult<int>> CreateTestUser([FromBody] CreateTestUserCommand command)
    {
        return await Mediator.Send(command);
    }

    [HttpPost("check-username-availability")]
    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK, HttpContentTypes.TextPlain)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest, HttpContentTypes.ProblemJson)]
    public async Task<ActionResult<bool>> CheckUsernameAvailable([FromBody] CheckUsernameAvailableQuery query)
    {
        return await Mediator.Send(query);
    }
}
