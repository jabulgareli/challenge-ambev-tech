using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.CalculateDiscount
{
    internal class CalculateDiscountValidator : AbstractValidator<CalculateDiscountCommand>
    {
        public CalculateDiscountValidator()
        {
            RuleFor(sale => sale.Items).NotEmpty().ForEach(item => item.SetValidator(new CalculateDiscountItemRequestValidator()));
        }
    }

    internal class CalculateDiscountItemRequestValidator : AbstractValidator<CalculateDiscountItemCommand>
    {
        public CalculateDiscountItemRequestValidator()
        {
            RuleFor(item => item.ProductId).NotEmpty();
            RuleFor(item => item.ProductName).NotEmpty().Length(1, 200);
            RuleFor(item => item.Quantity).GreaterThan(0);
            RuleFor(item => item.UnitPrice).GreaterThan(0);
        }
    }
}
