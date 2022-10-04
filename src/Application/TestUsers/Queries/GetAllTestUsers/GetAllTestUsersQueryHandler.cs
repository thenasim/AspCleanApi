using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.TestUsers.Queries.GetAllTestUsers;

public class GetAllTestUsersQueryHandler : IRequestHandler<GetAllTestUsersQuery, List<TestUser>>
{
    private readonly IApplicationDbContext _context;

    public GetAllTestUsersQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<TestUser>> Handle(GetAllTestUsersQuery request, CancellationToken cancellationToken)
    {
        return await _context.TestUsers.ToListAsync();
    }
}
