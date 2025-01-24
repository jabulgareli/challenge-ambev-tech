namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CalculateDiscount
{
    public class CalculateDiscountResponse
    {
        public IEnumerable<CalculateDiscountItemResponse> Items { get; set; } = [];
    }

    public class CalculateDiscountItemResponse
    {
        public Guid ProductId { get; set; }

        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }

        public decimal Discount { get; set; }
        public decimal TotalAmount { get; set; }
    }
}