using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CalculateDiscount
{
    internal class CalculateDiscountRequestValidator : AbstractValidator<CalculateDiscountRequest>
    {
        public CalculateDiscountRequestValidator()
        {
            RuleFor(sale => sale.Items).NotEmpty().ForEach(item => item.SetValidator(new CalculateDiscountItemRequestValidator()));
        }
    }

    internal class CalculateDiscountItemRequestValidator : AbstractValidator<CalculateDiscountItemRequest>
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
