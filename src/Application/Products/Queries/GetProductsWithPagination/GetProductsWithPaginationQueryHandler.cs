using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Products.Queries.GetProductsWithPagination;

public class GetProductsWithPaginationQueryHandler : IRequestHandler<GetProductsWithPaginationQuery, List<Product>>
{
    private readonly IApplicationDbContext _context;

    public GetProductsWithPaginationQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task<List<Product>> Handle(GetProductsWithPaginationQuery request, CancellationToken cancellationToken)
    {
        var products = await _context.Products.ToListAsync(cancellationToken);

        return products;
    }
}
