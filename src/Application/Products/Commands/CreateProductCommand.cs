using MediatR;

namespace Application.Products.Commands;

public class CreateProductCommand : IRequest<int>
{
    public string Name { get; set; } = null!;
    public int Quantity { get; set; }
}
