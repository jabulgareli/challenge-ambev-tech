using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Results;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.DeleteSale
{
    public class DeleteSaleCommandHandler(
        ISaleRepository repository) : IRequestHandler<DeleteSaleCommand, Result>
    {
        public async Task<Result> Handle(DeleteSaleCommand request, CancellationToken cancellationToken)
        {
            var sale = await repository.GetByIdAsync(request.Id, cancellationToken);

            if (sale is null) return Result.Fail("Sale not found", 404);

            await repository.DeleteAsync(sale, cancellationToken);

            return Result.Success();
        }
    }
}
