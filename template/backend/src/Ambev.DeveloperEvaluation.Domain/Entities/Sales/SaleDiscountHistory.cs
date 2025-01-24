namespace Ambev.DeveloperEvaluation.Domain.Entities.Sales
{
    public class SaleDiscountHistory
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime CalculatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<SaleDiscountItemHistory> Items { get; set; } = [];
    }

    public class SaleDiscountItemHistory
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Discount { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
