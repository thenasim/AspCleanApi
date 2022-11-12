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
                modelState.AddModelError(error.Code, error.Description);
            }
            return ValidationProblem(modelState);
        }

        var (statusCode, errorMessage) = exception switch
        {
            AuthenticationException => (StatusCodes.Status403Forbidden, "You are not authorized to perform this action."),
            NotImplementedException => (StatusCodes.Status501NotImplemented, "Feature is not implemented."),
            InvalidCastException => (StatusCodes.Status500InternalServerError, "Unable to cast from one type to another type."),
            _ => (StatusCodes.Status500InternalServerError, "Unknown error has occurred.")
        };

        return Problem(errorMessage, null, statusCode);
    }
}
