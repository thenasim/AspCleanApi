using Application.Common.Interfaces;
using Domain.Events;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Orders.Commands;

public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, int>
{
    private readonly IApplicationDbContext _context;

    public CreateOrderCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var product = await _context.Products
            .AsTracking()
            .FirstOrDefaultAsync(x => x.Id == request.ProductId);

        if (product is null)
        {
            throw new ValidationException("Invalid product Id");
        }

        product.AddDomainEvent(new OrderCreated(product, request.Quantity));
        await _context.SaveChangesAsync(cancellationToken);

        return 1;
    }
}
