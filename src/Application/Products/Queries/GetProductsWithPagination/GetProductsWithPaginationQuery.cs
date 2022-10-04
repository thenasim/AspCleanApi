using System.ComponentModel;
using Domain.Entities;
using MediatR;

namespace Application.Products.Queries.GetProductsWithPagination;

public class GetProductsWithPaginationQuery : IRequest<List<Product>>
{
    [DefaultValue(1)]
    public int PageNumber { get; set; } = 1;

    [DefaultValue(10)]
    public int PageSize { get; set; } = 10;
}
