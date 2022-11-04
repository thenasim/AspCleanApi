using Domain.Enums;

namespace Application.TestUsers.Responses;

public class TestUserResponse
{
    public int Id { get; set; }
    public string FullName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string ShortIntro { get; set; } = null!;
    public Gender Gender { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public DateTime Created { get; set; }
}
