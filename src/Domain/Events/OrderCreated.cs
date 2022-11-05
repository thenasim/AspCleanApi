using Domain.Common;
using Domain.Entities;

namespace Domain.Events;

public class OrderCreatedEvent : BaseEvent
{
    public OrderCreatedEvent(Product product, int quantity)
    {
        Product = product;
        Quantity = quantity;
    }

    public Product Product { get; }
    public int Quantity { get; }
}
