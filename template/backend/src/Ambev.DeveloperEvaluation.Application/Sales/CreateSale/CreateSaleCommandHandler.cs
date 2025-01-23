using Ambev.DeveloperEvaluation.Domain.Entities.Sales;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Services;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale
{
    public class CreateSaleHandler(
        ISaleRepository repository,
        IMapper mapper,
        IEventBusService eventBusService) : IRequestHandler<CreateSaleCommand, CreateSaleResult>
    {
        public async Task<CreateSaleResult> Handle(CreateSaleCommand command, CancellationToken cancellationToken)
        {
            var validator = new CreateSaleValidator();
            var validationResult = await validator.ValidateAsync(command, cancellationToken);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var sale = mapper.Map<Sale>(command);

            sale.AggregateItems();

            if (sale.HasItemsWithInvalidQuantity())
                throw new ValidationException("Some items have invalid quantities. Please check the quantities and try again.");

            sale.ApplyDiscounts();

            var createdSale = await repository.CreateAsync(sale);

            sale.Finish();

            //Fire and forget
            _ = Task.Run(() => PublishEventsAsync(sale));

            var result = mapper.Map<CreateSaleResult>(createdSale);
            return result;
        }

        private async Task PublishEventsAsync(Sale sale)
        {
            foreach(var saleEvent in sale.DomainEvents)
            {
                await eventBusService.PublishAsync(saleEvent.GetType().Name, saleEvent);
            }
        }
    }
}
