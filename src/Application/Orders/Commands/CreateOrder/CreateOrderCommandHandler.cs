using Application.Common.Interfaces;
using Domain.Entities;
using Domain.Events;
using FluentValidation;
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
        _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.TrackAll;

        // Check if product exists
        var product = await _context.Products.FirstOrDefaultAsync(x => x.Id == request.ProductId, cancellationToken);
        if (product is null)
        {
            throw new ValidationException("Invalid product Id");
        }

        // Create order
        var order = new Order
        {
            ProductId = request.ProductId,
            TotalProductOrdered = request.Quantity
        };
        _context.Orders.Add(order);

        // Send domain events
        product.AddDomainEvent(new OrderCreated(product, request.Quantity));

        await _context.SaveChangesAsync(cancellationToken);

        return order.Id;
    }
}
