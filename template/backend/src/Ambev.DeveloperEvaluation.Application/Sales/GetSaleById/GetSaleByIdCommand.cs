using Ambev.DeveloperEvaluation.Domain.Results;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSaleById
{
    public record GetSaleByIdCommand(Guid Id) : IRequest<DataResult<GetSaleByIdResult>>;
}
