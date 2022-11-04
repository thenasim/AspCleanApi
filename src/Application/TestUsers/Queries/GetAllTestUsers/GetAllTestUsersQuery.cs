using Application.TestUsers.Responses;
using MediatR;

namespace Application.TestUsers.Queries.GetAllTestUsers;

public class GetAllTestUsersQuery : IRequest<List<TestUserResponse>>
{
}
