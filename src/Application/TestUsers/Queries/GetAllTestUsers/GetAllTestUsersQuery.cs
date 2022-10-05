using Domain.Entities;
using MediatR;

namespace Application.TestUsers.Queries.GetAllTestUsers;

public class GetAllTestUsersQuery : IRequest<List<TestUser>>
{
}