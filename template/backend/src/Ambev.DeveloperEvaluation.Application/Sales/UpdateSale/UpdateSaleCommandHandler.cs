using Ambev.DeveloperEvaluation.Domain.Entities.Sales;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Results;
using Ambev.DeveloperEvaluation.Domain.Services;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale
{
    public class UpdateSaleHandler(
        ISaleRepository repository,
        IMapper mapper,
        IEventBusService eventBusService) : IRequestHandler<UpdateSaleCommand, Result>
    {
        public async Task<Result> Handle(UpdateSaleCommand command, CancellationToken cancellationToken)
        {
            var validator = new UpdateSaleValidator();
            var validationResult = await validator.ValidateAsync(command, cancellationToken);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var sale = mapper.Map<Sale>(command);

            var createdSale = await repository.GetByIdAsync(command.Id, cancellationToken);

            if (createdSale is null) return Result.Fail("Sale not found", 404);

            createdSale.Modify(sale);

            createdSale.AggregateItems();

            if (createdSale.HasItemsWithInvalidQuantity())
                return Result.Fail("Some items have invalid quantities. Please check the quantities and try again.", 400);

            createdSale.ApplyDiscounts();

            await repository.UpdateAsync(createdSale, cancellationToken);

            //Fire and forget
            _ = Task.Run(() => PublishEventsAsync(createdSale));

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
