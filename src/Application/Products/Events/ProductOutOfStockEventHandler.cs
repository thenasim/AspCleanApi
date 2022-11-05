using Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Products.Events;

public class ProductOutOfStockEventHandler : INotificationHandler<ProductOutOfStockEvent>
{
    private readonly ILogger<ProductOutOfStockEventHandler> _logger;

    public ProductOutOfStockEventHandler(ILogger<ProductOutOfStockEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(ProductOutOfStockEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogWarning($"Product with id: {notification.Product.Id} is out of stock and has {notification.Product.Quantity} items available.");

        return Task.CompletedTask;
    }
}
