using FluentValidation.Results;

namespace Application.Common.Exceptions;

public class AppValidationException : Exception
{
    public List<ValidationFailure>? Errors { get; }

    private AppValidationException() : base("One or more validation errors have occurred.")
    {
    }

    public AppValidationException(List<ValidationFailure>? errors) : this()
    {
        Errors = errors;
    }
}
