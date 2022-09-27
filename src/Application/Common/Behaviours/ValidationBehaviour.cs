﻿using FluentValidation;
using MediatR;

namespace Application.Common.Behaviours;

public class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly IValidator<TRequest>? _validator;

    public ValidationBehaviour(IValidator<TRequest>? validator = null)
    {
        _validator = validator;
    }
    
    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
    {
        if (_validator is null)
        {
            return await next();
        }

        var validationResult = await _validator.ValidateAsync(request, cancellationToken);

        if (validationResult.IsValid == false)
        {
            throw new ValidationException(validationResult.Errors.FirstOrDefault()?.ErrorMessage ??
                                          "Unknown error occured");
        }
        
        return await next();
    }
}
