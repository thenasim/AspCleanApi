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
    public async Task<IActionResult> GetAllTestUsers()
    {
        var result = await Mediator.Send(new GetAllTestUsersQuery());

        return result.Match(
          value => Ok(value),
          errors => Problem(errors)
        );
    }

    [HttpPost]
    [ProducesResponseType(typeof(int), StatusCodes.Status201Created, HttpContentTypes.TextPlain)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest, HttpContentTypes.ProblemJson)]
    public async Task<IActionResult> CreateTestUser([FromBody] CreateTestUserCommand command)
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

    [HttpPost("is-username-available")]
    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK, HttpContentTypes.TextPlain)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest, HttpContentTypes.ProblemJson)]
    public async Task<IActionResult> CheckUsernameAvailable([FromBody] CheckUsernameAvailableQuery query)
    {
        var result = await Mediator.Send(query);

        return result.Match(
          value => Ok(value),
          errors => Problem(errors)
        );
    }
}
