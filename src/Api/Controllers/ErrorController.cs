using System.Security.Authentication;
using Application.Common.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Api.Controllers;

/// <summary>
/// https://learn.microsoft.com/en-us/aspnet/core/fundamentals/error-handling
/// </summary>
[ApiExplorerSettings(IgnoreApi = true)]
public class ErrorController : ControllerBase
{
    [Route("/error")]
    public IActionResult Error()
    {
        var exception = HttpContext.Features.Get<IExceptionHandlerFeature>()?.Error;

        if (exception is AppValidationException validationException)
        {
            if (validationException.Errors is null)
            {
                return ValidationProblem(exception?.Message, null, StatusCodes.Status400BadRequest);
            }

            var modelState = new ModelStateDictionary();
            foreach (var error in validationException.Errors)
            {
                modelState.AddModelError(error.PropertyName, error.ErrorMessage);
            }
            return ValidationProblem(modelState);
        }

        var statusCode = exception switch
        {
            AuthenticationException => StatusCodes.Status403Forbidden,
            NotImplementedException => StatusCodes.Status501NotImplemented,
            _ => StatusCodes.Status500InternalServerError
        };

        return Problem(exception?.Message, null, statusCode);
    }
}
