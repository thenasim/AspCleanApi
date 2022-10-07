using Domain.Common;

namespace Domain.Entities;

public class Order : BaseAuditableEntity
{
    public Order()
    {
    }

    public Order(int productId, int orderedQuantity, double productPrice)
    {
        ProductId = productId;
        OrderedQuantity = orderedQuantity;
        OrderedPrice = orderedQuantity * productPrice;
    }

    public int ProductId { get; init; }
    public int OrderedQuantity { get; init; }
    public double OrderedPrice { get; init; }
}
