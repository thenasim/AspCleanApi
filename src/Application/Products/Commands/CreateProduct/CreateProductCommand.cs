using Application.Common.Interfaces;

namespace Application.Products.Commands.CreateProduct;

public class CreateProductCommand : ICommand<int>
{
    public string Name { get; set; } = null!;
    public double Price { get; set; }
    public int Quantity { get; set; }
}
