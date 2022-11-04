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
        // Check if product exists
        var product = await _context.Products
            .AsTracking()
            .FirstAsync(x => x.Id == request.ProductId, cancellationToken);

        // Create order
        using var transaction = _context.Database.BeginTransaction();
        var order = new Order(request.ProductId, request.Quantity, product.Price);
        _context.Orders.Add(order);

        // Send domain events
        product.AddDomainEvent(new OrderCreated(product, request.Quantity));

        await _context.SaveChangesAsync(cancellationToken);
        transaction.Commit();

        return order.Id;
    }
}
