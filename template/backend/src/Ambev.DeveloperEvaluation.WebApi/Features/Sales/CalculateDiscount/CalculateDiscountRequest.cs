using System.Collections;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CalculateDiscount
{
    public class CalculateDiscountRequest
    {
        public IEnumerable<CalculateDiscountItemRequest> Items { get; set; } = [];
    }

    public class CalculateDiscountItemRequest
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
