using ErrorOr;
using FluentValidation.Results;

namespace Application.Common.Exceptions;

public class AppValidationException : Exception
{
    public List<Error>? Errors { get; }

    private AppValidationException() : base("One or more validation errors have occurred.")
    {
    }

    public AppValidationException(List<Error>? errors) : this()
    {
        Errors = errors;
    }
}
