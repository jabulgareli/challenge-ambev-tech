using Ambev.DeveloperEvaluation.Domain.Results;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.CanceItem
{
    public record CancelSaleItemCommand(Guid Id, Guid ProductId) : IRequest<Result>;
}
