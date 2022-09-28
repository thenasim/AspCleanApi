using Application.Common.Interfaces;
using Domain.Events;
using MediatR;

namespace Application.Orders.Events;

public class OrderCreatedEventHandler : INotificationHandler<OrderCreated>
{
    private readonly IApplicationDbContext _context;

    public OrderCreatedEventHandler(IApplicationDbContext context)
    {
        _context=context;
    }

    public Task Handle(OrderCreated notification, CancellationToken cancellationToken)
    {
        notification.Product.Quantity -= notification.Quantity;

        return Task.CompletedTask;
    }
}
