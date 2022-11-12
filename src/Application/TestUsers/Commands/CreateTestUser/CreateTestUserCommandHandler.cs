using Application.Common.Interfaces;
using Domain.Entities;
using ErrorOr;
using MediatR;

namespace Application.TestUsers.Commands.CreateTestUser;

public class CreateTestUserCommandHandler : IRequestHandler<CreateTestUserCommand, ErrorOr<int>>
{
    private readonly IApplicationDbContext _context;

    public CreateTestUserCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ErrorOr<int>> Handle(CreateTestUserCommand request, CancellationToken cancellationToken)
    {
        var testUser = new TestUser
        {
            FullName = request.FullName!,
            Email = request.Email!,
            Username = request.Username!,
            DateOfBirth = request.DateOfBirth,
            Gender = request.Gender
        };

        await _context.TestUsers.AddAsync(testUser);
        await _context.SaveChangesAsync(cancellationToken);

        return testUser.Id;
    }
}
