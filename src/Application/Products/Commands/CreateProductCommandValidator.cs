﻿using FluentValidation;

namespace Application.Products.Commands;

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(x => x.Name)
            .MinimumLength(2)
            .MaximumLength(200);
        
        RuleFor(x => x.Quantity)
            .GreaterThan(0);
    }
}
