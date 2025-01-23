using Ambev.DeveloperEvaluation.Domain.Entities.Sales;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Results;
using Ambev.DeveloperEvaluation.Domain.Services;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.CancelSale
{
    public class CancelSaleCommandHandler(
        ISaleRepository repository,
        IEventBusService eventBusService) : IRequestHandler<CancelSaleCommand, Result>
    {
        public async Task<Result> Handle(CancelSaleCommand request, CancellationToken cancellationToken)
        {
            var sale = await repository.GetByIdAsync(request.Id, cancellationToken);

            if (sale is null) return Result.Fail("Sale not found", 404);

            if (!sale.CanCancel()) return Result.Fail("Sale cannot be canceled", 400);

            sale.Cancel();

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
