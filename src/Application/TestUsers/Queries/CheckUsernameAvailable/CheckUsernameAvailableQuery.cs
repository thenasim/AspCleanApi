using MediatR;

namespace Application.TestUsers.Queries.CheckUsernameAvailable;

public class CheckUsernameAvailableQuery : IRequest<bool>
{
    public string? Username { get; set; }
}
