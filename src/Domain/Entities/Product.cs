using Domain.Common;

namespace Domain.Entities;

public class Product : BaseAuditableEntity
{
    public string Name { get; set; } = null!;
    public double Price { get; set; }
    public int Quantity { get; set; }
}
