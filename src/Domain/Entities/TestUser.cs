using Domain.Common;
using Domain.Enums;

namespace Domain.Entities;

public class TestUser : BaseAuditableEntity
{
    public string FullName { get; set; } = null!;
    public Gender Gender { get; set; }
    public DateOnly DateOfBirth { get; set; }
}
