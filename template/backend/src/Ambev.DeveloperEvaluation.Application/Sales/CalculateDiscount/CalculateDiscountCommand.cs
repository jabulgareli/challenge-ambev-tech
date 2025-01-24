using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.CalculateDiscount
{
    public class CalculateDiscountCommand : IRequest<CalculateDiscountResult>
    {
        public IEnumerable<CalculateDiscountItemCommand> Items { get; set; } = [];
    }

    public class CalculateDiscountItemCommand
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }

}
