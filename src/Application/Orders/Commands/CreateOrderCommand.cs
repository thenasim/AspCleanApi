using MediatR;

namespace Application.Orders.Commands;

public class CreateOrderCommand : IRequest<int>
{
    public int ProductId { get; set; }
    public int Quantity { get; set; }
}
