namespace Application.Products.Responses;

public class ProductResponse
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public double Price { get; set; }
    public int Quantity { get; set; }
    public bool IsOutOfStock { get; set; }
    public DateTime Created { get; set; }
    public DateTime? LastModified { get; set; }
}
