using Application.Common.Interfaces;
using Application.TestUsers.Responses;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.TestUsers.Queries.GetAllTestUsers;

public class GetAllTestUsersQueryHandler : IRequestHandler<GetAllTestUsersQuery, List<TestUserResponse>>
{
    private readonly IApplicationDbContext _context;

    public GetAllTestUsersQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<TestUserResponse>> Handle(GetAllTestUsersQuery request, CancellationToken cancellationToken)
    {
        var users = await _context.TestUsers
            .ToListAsync();

        return users.Adapt<List<TestUserResponse>>();
    }
}
