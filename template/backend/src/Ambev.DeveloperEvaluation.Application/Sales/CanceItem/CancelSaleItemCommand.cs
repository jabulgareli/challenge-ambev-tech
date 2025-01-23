using Ambev.DeveloperEvaluation.Domain.Results;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.CancelSale
{
    public record CancelSaleItemCommand(Guid Id, Guid ProductId) : IRequest<Result>;
}
