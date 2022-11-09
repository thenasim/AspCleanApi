using Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.TestUsers.Queries.CheckUsernameAvailable;

public class CheckUsernameAvailableQueryHandler : IRequestHandler<CheckUsernameAvailableQuery, bool>
{
    private readonly IApplicationDbContext _context;

    public CheckUsernameAvailableQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(CheckUsernameAvailableQuery request, CancellationToken cancellationToken)
    {
        return await _context.TestUsers.AllAsync(x => x.Username != request.Username);
    }
}
