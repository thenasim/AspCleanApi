using Application.Common.Interfaces;
using Domain.Entities;
using Domain.Events;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Orders.Commands.CreateOrder;

public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, int>
{
    private readonly IApplicationDbContext _context;

    public CreateOrderCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        // Load product
        var product = await _context.Products
            .AsTracking()
            .FirstAsync(x => x.Id == request.ProductId, cancellationToken);

        // Create order
        using var transaction = _context.Database.BeginTransaction();
        var order = new Order(request.ProductId, request.Quantity, product.Price);
        _context.Orders.Add(order);

        // Reduce quantity
        product.ReduceQuantity(request.Quantity);

        // Send domain events
        product.AddDomainEvent(new OrderCreatedEvent(product, request.Quantity));

        await _context.SaveChangesAsync(cancellationToken);
        transaction.Commit();

        return order.Id;
    }
}
