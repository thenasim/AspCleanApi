using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Products.Queries;

public class GetProductsWithPaginationHandler : IRequestHandler<GetProductsWithPaginationQuery, List<Product>>
{
    private readonly IApplicationDbContext _context;

    public GetProductsWithPaginationHandler(IApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task<List<Product>> Handle(GetProductsWithPaginationQuery request, CancellationToken cancellationToken)
    {
        var products = await _context.Products.ToListAsync(cancellationToken);

        return products;
    }
}
