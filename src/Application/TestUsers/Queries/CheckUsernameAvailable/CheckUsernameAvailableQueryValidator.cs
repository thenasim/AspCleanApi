using FluentValidation;

namespace Application.TestUsers.Queries.CheckUsernameAvailable;

public class CheckUsernameAvailableQueryValidator : AbstractValidator<CheckUsernameAvailableQuery>
{
    public CheckUsernameAvailableQueryValidator()
    {
        RuleFor(x => x.Username)
            .NotEmpty()
            .MinimumLength(4)
            .MaximumLength(20);
    }
}
