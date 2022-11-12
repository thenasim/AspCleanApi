using Application.Common.Interfaces;
using Application.Products.Responses;
using ErrorOr;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Products.Queries.GetProductsWithPagination;

public class GetProductsWithPaginationQueryHandler : IRequestHandler<GetProductsWithPaginationQuery, ErrorOr<List<ProductResponse>>>
{
    private readonly IApplicationDbContext _context;

    public GetProductsWithPaginationQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ErrorOr<List<ProductResponse>>> Handle(GetProductsWithPaginationQuery request, CancellationToken cancellationToken)
    {
        return await _context.Products
            .ProjectToType<ProductResponse>()
            .ToListAsync(cancellationToken);
    }
}
