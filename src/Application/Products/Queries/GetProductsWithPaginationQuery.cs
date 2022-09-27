using Domain.Entities;
using MediatR;

namespace Application.Products.Queries;

public class GetProductsWithPaginationQuery : IRequest<List<Product>>
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}
