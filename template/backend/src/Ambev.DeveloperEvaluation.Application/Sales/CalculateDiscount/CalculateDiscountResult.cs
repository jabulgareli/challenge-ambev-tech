namespace Ambev.DeveloperEvaluation.Application.Sales.CalculateDiscount
{
    public class CalculateDiscountResult
    {
        public IEnumerable<CalculateDiscountItemResult> Items { get; set; } = [];
    }

    public class CalculateDiscountItemResult
    {
        public Guid ProductId { get; set; }

        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }

        public decimal Discount { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
