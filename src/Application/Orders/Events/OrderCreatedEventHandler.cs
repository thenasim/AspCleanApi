using Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Orders.Events;

public class OrderCreatedEventHandler : INotificationHandler<OrderCreated>
{
    private readonly ILogger<OrderCreatedEventHandler> _logger;

    public OrderCreatedEventHandler(ILogger<OrderCreatedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(OrderCreated notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Product with id: {notification.Product.Id} has been ordered with {notification.Quantity} items.");

        return Task.CompletedTask;
    }
}
