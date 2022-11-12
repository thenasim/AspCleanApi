using Application.Common.Interfaces;

namespace Application.Orders.Commands.CreateOrder;

public class CreateOrderCommand : ICommand<int>
{
    public int ProductId { get; set; }
    public int Quantity { get; set; }
}
