using ErrorOr;
using MediatR;

namespace Application.Common.Interfaces;

public class IQuery<TResponse> : IRequest<ErrorOr<TResponse>>
{
}
