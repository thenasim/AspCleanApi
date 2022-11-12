using Application.Common.Interfaces;

namespace Application.TestUsers.Queries.CheckUsernameAvailable;

public class CheckUsernameAvailableQuery : IQuery<bool>
{
    public string? Username { get; set; }
}
