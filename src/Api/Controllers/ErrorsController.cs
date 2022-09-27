using System.Net;
using System.Security.Authentication;
using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

/// <summary>
/// https://learn.microsoft.com/en-us/aspnet/core/fundamentals/error-handling?view=aspnetcore-6.0
/// </summary>
public class ErrorsController : ControllerBase
{
    [Route("/error")]
    public IActionResult Error()
    {
        var exception = HttpContext.Features.Get<IExceptionHandlerFeature>()?.Error;

        var statusCode = exception switch
        {
            ValidationException => HttpStatusCode.BadRequest,
            AuthenticationException => HttpStatusCode.Forbidden,
            NotImplementedException => HttpStatusCode.NotImplemented,
            _ => HttpStatusCode.InternalServerError
        };
        
        return Problem(exception?.Message, null, (int)statusCode);
    }
}
