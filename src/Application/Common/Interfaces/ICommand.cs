using ErrorOr;
using MediatR;

namespace Application.Common.Interfaces;

public class ICommand<TResponse> : IRequest<ErrorOr<TResponse>>
{
}
