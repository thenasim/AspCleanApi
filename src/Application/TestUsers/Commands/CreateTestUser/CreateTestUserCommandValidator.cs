using FluentValidation;

namespace Application.TestUsers.Commands.CreateTestUser;

public class CreateTestUserCommandValidator : AbstractValidator<CreateTestUserCommand>
{
    public CreateTestUserCommandValidator()
    {
        RuleFor(x => x.FullName)
            .NotEmpty()
            .MinimumLength(2)
            .MaximumLength(200);

        RuleFor(x => x.DateOfBirth)
            .LessThanOrEqualTo(DateOnly.FromDateTime(DateTime.Now.AddYears(-18)))
            .WithMessage("Date must be 18 years before")
            .GreaterThanOrEqualTo(DateOnly.FromDateTime(DateTime.Now.AddYears(-100)))
            .WithMessage("Date must be 100 years later");

        RuleFor(x => x.Gender)
            .IsInEnum();
    }
}
