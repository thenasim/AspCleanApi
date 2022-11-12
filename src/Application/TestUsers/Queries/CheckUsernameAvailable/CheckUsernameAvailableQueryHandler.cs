using Application.Common.Interfaces;
using ErrorOr;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.TestUsers.Queries.CheckUsernameAvailable;

public class CheckUsernameAvailableQueryHandler : IRequestHandler<CheckUsernameAvailableQuery, ErrorOr<bool>>
{
    private readonly IApplicationDbContext _context;

    public CheckUsernameAvailableQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ErrorOr<bool>> Handle(CheckUsernameAvailableQuery request, CancellationToken cancellationToken)
    {
        return await _context.TestUsers.AllAsync(x => x.Username != request.Username);
    }
}
