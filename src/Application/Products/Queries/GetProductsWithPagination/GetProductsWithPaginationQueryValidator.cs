using FluentValidation;

namespace Application.Products.Queries.GetProductsWithPagination;

public class GetProductsWithPaginationQueryValidator : AbstractValidator<GetProductsWithPaginationQuery>
{
    public GetProductsWithPaginationQueryValidator()
    {
        RuleFor(x => x.PageNumber)
            .GreaterThan(0);
        
        RuleFor(x => x.PageSize)
            .GreaterThan(10)
            .LessThanOrEqualTo(200);
    }
}
