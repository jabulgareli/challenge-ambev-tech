using Ambev.DeveloperEvaluation.Domain.Results;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.DeleteSale
{
    public record DeleteSaleCommand(Guid Id) : IRequest<Result>;
}
