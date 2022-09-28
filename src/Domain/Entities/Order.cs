using Domain.Common;

namespace Domain.Entities;

public class Order : BaseAuditableEntity
{
    public int ProductId { get; set; }
    public int TotalProductOrdered { get; set; }
}
