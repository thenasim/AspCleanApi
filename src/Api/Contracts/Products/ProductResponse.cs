namespace Api.Contracts.Products;

public class ProductResponse
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public int Quantity { get; set; }
    public DateTime Created { get; set; }
    public DateTime? LastModified { get; set; }
}
