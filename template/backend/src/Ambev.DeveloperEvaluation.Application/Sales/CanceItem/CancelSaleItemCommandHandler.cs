using Ambev.DeveloperEvaluation.Domain.Entities.Sales;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Results;
using Ambev.DeveloperEvaluation.Domain.Services;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.CancelSale
{
    public class CancelSaleItemCommandHandler(
        ISaleRepository repository,
        IEventBusService eventBusService) : IRequestHandler<CancelSaleItemCommand, Result>
    {
        public async Task<Result> Handle(CancelSaleItemCommand request, CancellationToken cancellationToken)
        {
            var sale = await repository.GetByIdAsync(request.Id, cancellationToken);

            if (sale is null) return Result.Fail("Sale not found", 404);

            if (!sale.CanCancel()) return Result.Fail("Sale cannot be canceled", 400);

            if (!sale.CancelItem(request.ProductId)) return Result.Fail("Item not found or already canceled", 400);

            await repository.UpdateAsync(sale, cancellationToken);

            //Fire and forget
            _ = Task.Run(() => PublishEventsAsync(sale));

            return Result.Success();
        }

        private async Task PublishEventsAsync(Sale sale)
        {
            foreach (var saleEvent in sale.DomainEvents)
            {
                await eventBusService.PublishAsync(saleEvent.GetType().Name, saleEvent);
            }
        }
    }
}
