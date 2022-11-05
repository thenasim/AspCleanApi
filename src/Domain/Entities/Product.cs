using Domain.Common;
using Domain.Events;

namespace Domain.Entities;

public class Product : BaseAuditableEntity
{
    public string Name { get; set; } = null!;
    public double Price { get; set; }

    private int _quantity;
    public int Quantity
    {
        get => _quantity;
        set
        {
            _quantity = value;

            if (_quantity == 0)
            {
                IsOutOfStock = true;
            }
        }
    }

    private bool _isOutOfStock { get; set; }
    public bool IsOutOfStock
    {
        get => _isOutOfStock;
        set
        {
            _isOutOfStock = value;

            if (_isOutOfStock)
            {
                AddDomainEvent(new ProductOutOfStockEvent(this));
            }
        }
    }
}
