using Application.Common.Exceptions;
using FluentValidation;
using MediatR;

namespace Application.Common.Behaviors;

public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
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
        var failures = validationResults
            .Where(x => x.Errors.Count > 0)
            .SelectMany(x => x.Errors)
            .ToList();

        if (failures.Count > 0)
        {
            throw new AppValidationException(failures);
        }

        return await next();
    }
}
