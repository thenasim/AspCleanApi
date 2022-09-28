using Domain.Common;
using Domain.Entities;

namespace Domain.Events;

public class OrderCreated : BaseEvent
{
    public OrderCreated(Product product, int quantity)
    {
        Product = product;
        Quantity = quantity;
    }

    public Product Product { get; }
    public int Quantity { get; }
}
