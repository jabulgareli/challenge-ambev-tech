using Ambev.DeveloperEvaluation.Domain.Entities.Sales;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Results;
using AutoMapper;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.ListSales
{
    public class ListSalesCommandHandler(
        ISaleRepository repository,
        IMapper mapper) : IRequestHandler<ListSalesCommand, PagedResult<ListSalesResult>>
    {
        public async Task<PagedResult<ListSalesResult>> Handle(ListSalesCommand request, CancellationToken cancellationToken)
        {
            var sales = await repository.SearchAsync(
                request.Page,
                request.PageSize,
                request.CustomerId,
                request.BranchId,
                request.SaleNumber,
                cancellationToken);

            var foundSales = mapper.Map<IEnumerable<Sale>, IEnumerable<ListSalesResult>>(sales.sales);

            if (!foundSales.Any()) return PagedResult<ListSalesResult>.Fail("There are no data for filter", 404);

            return PagedResult<ListSalesResult>.Success(foundSales, request.Page, request.PageSize, sales.count);
        }
    }
}
