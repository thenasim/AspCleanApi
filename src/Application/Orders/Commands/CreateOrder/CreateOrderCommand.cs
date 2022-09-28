using MediatR;

namespace Application.Orders.Commands.CreateOrder;

public class CreateOrderCommand : IRequest<int>
{
    public int ProductId { get; set; }
    public int Quantity { get; set; }
}
