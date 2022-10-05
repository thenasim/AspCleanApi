using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.TestUsers.Commands.CreateTestUser;

public class CreateTestUserCommandHandler : IRequestHandler<CreateTestUserCommand, int>
{
    private readonly IApplicationDbContext _context;

    public CreateTestUserCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateTestUserCommand request, CancellationToken cancellationToken)
    {
        var testUser = new TestUser
        {
            FullName = request.FullName,
            DateOfBirth = request.DateOfBirth,
            Gender = request.Gender
        };

        await _context.TestUsers.AddAsync(testUser);
        await _context.SaveChangesAsync(cancellationToken);

        return testUser.Id;
    }
}