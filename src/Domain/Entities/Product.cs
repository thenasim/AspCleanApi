using Domain.Common;

namespace Domain.Entities;

public class Product : BaseAuditableEntity
{
    public string Name { get; set; } = null!;
    public int Quantity { get; set; }
}
