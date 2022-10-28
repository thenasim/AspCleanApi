using Domain.Enums;

namespace Api.Contracts.TestUsers;

public class TestUserResponse
{
    public int Id { get; set; }
    public string FullName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public Gender Gender { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public DateTime Created { get; set; }
}
