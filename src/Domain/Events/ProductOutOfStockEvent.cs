using Domain.Common;
using Domain.Entities;

namespace Domain.Events;

public class ProductOutOfStockEvent : BaseEvent
{
    public ProductOutOfStockEvent(Product product)
    {
        Product = product;
    }

    public Product Product { get; }
}
