using Application.Common.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Application.Orders.Commands.CreateOrder;

public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
{
    private readonly IApplicationDbContext _context;

    public CreateOrderCommandValidator(IApplicationDbContext context)
    {
        _context = context;

        RuleFor(x => x.ProductId)
            .Cascade(CascadeMode.Stop)
            .GreaterThan(0)
            .MustAsync(ProductExists)
            .WithMessage("No product found with this id.");

        RuleFor(x => x.Quantity)
            .Cascade(CascadeMode.Stop)
            .GreaterThan(0)
            .MustAsync(HasAvailableQuantity)
            .WithMessage("This product does not have any available requested quantity.");
    }

    public async Task<bool> ProductExists(int productId, CancellationToken cancellationToken)
    {
        return await _context.Products.AnyAsync(x => x.Id == productId, cancellationToken);
    }

    public async Task<bool> HasAvailableQuantity(
        CreateOrderCommand command,
        int requestedQuantity,
        ValidationContext<CreateOrderCommand> _,
        CancellationToken cancellationToken)
    {
        return await _context.Products.CountAsync(
            x => x.Id == command.ProductId && x.Quantity >= requestedQuantity,
            cancellationToken) == 1;
    }
}
