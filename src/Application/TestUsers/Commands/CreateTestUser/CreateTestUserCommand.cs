using Application.Common.Interfaces;
using Domain.Enums;

namespace Application.TestUsers.Commands.CreateTestUser;

public class CreateTestUserCommand : ICommand<int>
{
    public string? FullName { get; set; }
    public string? Email { get; set; }
    public string? Username { get; set; }
    public Gender Gender { get; set; }
    public DateOnly DateOfBirth { get; set; }
}
