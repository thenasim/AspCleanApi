using System.ComponentModel;
using Application.Products.Responses;
using MediatR;

namespace Application.Products.Queries.GetProductsWithPagination;

public class GetProductsWithPaginationQuery : IRequest<List<ProductResponse>>
{
    [DefaultValue(1)]
    public int PageNumber { get; set; } = 1;

    [DefaultValue(10)]
    public int PageSize { get; set; } = 10;
}
