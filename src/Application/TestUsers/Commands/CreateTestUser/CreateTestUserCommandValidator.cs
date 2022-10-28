using Application.Common.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Application.TestUsers.Commands.CreateTestUser;

public class CreateTestUserCommandValidator : AbstractValidator<CreateTestUserCommand>
{
    private readonly IApplicationDbContext _context;

    public CreateTestUserCommandValidator(IApplicationDbContext context)
    {
        _context = context;

        RuleFor(x => x.FullName)
            .NotEmpty()
            .MinimumLength(2)
            .MaximumLength(200);

        RuleFor(x => x.Email)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .EmailAddress()
            .MustAsync(BeUniqueEmail)
            .WithMessage("Email already exists.");

        RuleFor(x => x.DateOfBirth)
            .LessThanOrEqualTo(DateOnly.FromDateTime(DateTime.Now.AddYears(-18)))
            .WithMessage("Date must be 18 years before")
            .GreaterThanOrEqualTo(DateOnly.FromDateTime(DateTime.Now.AddYears(-100)))
            .WithMessage("Date must be 100 years later");

        RuleFor(x => x.Gender)
            .IsInEnum()
            .WithMessage("Invalid enum value.");
    }

    public async Task<bool> BeUniqueEmail(string email, CancellationToken cancellationToken)
    {
        return await _context.TestUsers.AllAsync(x => x.Email != email, cancellationToken);
    }
}
