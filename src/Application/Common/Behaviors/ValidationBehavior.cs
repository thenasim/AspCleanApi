using System.Reflection;
using Application.Common.Exceptions;
using ErrorOr;
using FluentValidation;
using MediatR;

namespace Application.Common.Behaviors;

public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : IErrorOr
{
    private readonly IEnumerable<IValidator<TRequest>>? _validator;

    public ValidationBehavior(IEnumerable<IValidator<TRequest>>? validator = null)
    {
        _validator = validator;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (_validator is null || _validator.Any() == false)
        {
            return await next();
        }

        var context = new ValidationContext<TRequest>(request);
        var validationResults = await Task.WhenAll(_validator.Select(v => v.ValidateAsync(context, cancellationToken)));
        var errors = validationResults
            .Where(x => x.Errors.Count > 0)
            .SelectMany(x => x.Errors)
            .Select(x => Error.Validation(x.PropertyName, x.ErrorMessage))
            .ToList();

        if (errors.Count > 0)
        {
            return TryCreateResponseFromErrors(errors, out var response)
                ? response
                : throw new AppValidationException(errors);
        }

        return await next();
    }

    private static bool TryCreateResponseFromErrors(List<Error> errors, out TResponse response)
    {
        response = (TResponse?)typeof(TResponse)
            .GetMethod(
                name: nameof(ErrorOr<object>.From),
                bindingAttr: BindingFlags.Static | BindingFlags.Public,
                types: new[] { typeof(List<Error>) })?
            .Invoke(null, new[] { errors })!;

        return response is not null;
    }
}
