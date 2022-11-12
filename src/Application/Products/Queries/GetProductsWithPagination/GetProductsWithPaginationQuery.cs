using System.ComponentModel;
using Application.Common.Interfaces;
using Application.Products.Responses;

namespace Application.Products.Queries.GetProductsWithPagination;

public class GetProductsWithPaginationQuery : IQuery<List<ProductResponse>>
{
    [DefaultValue(1)]
    public int PageNumber { get; set; } = 1;

    [DefaultValue(10)]
    public int PageSize { get; set; } = 10;
}
