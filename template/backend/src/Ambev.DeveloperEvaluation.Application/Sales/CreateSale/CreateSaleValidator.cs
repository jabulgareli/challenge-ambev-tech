using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale
{
    internal class CreateSaleValidator : AbstractValidator<CreateSaleCommand>
    {
        internal CreateSaleValidator()
        {
            RuleFor(sale => sale.SaleNumber).NotEmpty().Length(1, 200);
            RuleFor(sale => sale.SaleDate).GreaterThan(DateTime.MinValue).LessThanOrEqualTo(DateTime.UtcNow);
            RuleFor(sale => sale.CustomerId).NotEmpty();
            RuleFor(sale => sale.CustomerName).NotEmpty().Length(1, 200);
            RuleFor(sale => sale.BranchId).NotEmpty();
            RuleFor(sale => sale.BranchName).NotEmpty().Length(1, 200);
            RuleFor(sale => sale.Items).NotEmpty().ForEach(item => item.SetValidator(new SaleItemRequestValidator()));
        }
    }

    internal class SaleItemRequestValidator : AbstractValidator<SaleItemCommand>
    {
        internal SaleItemRequestValidator()
        {
            RuleFor(item => item.ProductId).NotEmpty();
            RuleFor(item => item.ProductName).NotEmpty().Length(1, 200);
            RuleFor(item => item.Quantity).GreaterThan(0);
            RuleFor(item => item.UnitPrice).GreaterThan(0);
        }
    }
}
