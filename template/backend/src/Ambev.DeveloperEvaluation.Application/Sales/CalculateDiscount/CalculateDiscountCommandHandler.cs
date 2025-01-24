using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Domain.Entities.Sales;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.CalculateDiscount
{
    public class CalculateDiscountCommandHandler(
        IMapper mapper,
        ISaleRepository saleRepository) : IRequestHandler<CalculateDiscountCommand, CalculateDiscountResult>
    {
        public async Task<CalculateDiscountResult> Handle(CalculateDiscountCommand command, CancellationToken cancellationToken)
        {
            var validator = new CalculateDiscountValidator();
            var validationResult = await validator.ValidateAsync(command, cancellationToken);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var sale = mapper.Map<Sale>(command);

            sale.AggregateItems();

            if (sale.HasItemsWithInvalidQuantity())
                throw new ValidationException("Some items have invalid quantities. Please check the quantities and try again.");

            sale.ApplyDiscounts();

            await saleRepository.CreateDiscountHistoryAsync(mapper.Map<SaleDiscountHistory>(sale), cancellationToken);

            var result = mapper.Map<CalculateDiscountResult>(sale);

            return result;
        }
    }
}
