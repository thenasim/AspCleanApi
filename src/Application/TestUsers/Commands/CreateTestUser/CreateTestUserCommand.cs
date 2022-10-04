using Domain.Enums;
using MediatR;

namespace Application.TestUsers.Commands.CreateTestUser;

public class CreateTestUserCommand : IRequest<int>
{
    public string FullName { get; set; } = null!;
    public Gender Gender { get; set; }
    public DateOnly DateOfBirth { get; set; }
}
