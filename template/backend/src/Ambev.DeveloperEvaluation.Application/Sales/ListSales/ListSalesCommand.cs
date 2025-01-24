using Ambev.DeveloperEvaluation.Domain.Results;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.ListSales
{
    public record ListSalesCommand(
        int Page = 1,
        int PageSize = 10,
        string? SaleNumber = null,
        Guid? CustomerId = null,
        Guid? BranchId = null) : IRequest<PagedResult<ListSalesResult>>;
}
