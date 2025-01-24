using Ambev.DeveloperEvaluation.Domain.Enums.Sales;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.ListSale
{
    public class ListSaleResponse
    {
        public string SaleNumber { get; set; } = string.Empty;
        public DateTime SaleDate { get; set; } = DateTime.MinValue;

        public Guid CustomerId { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public Guid BranchId { get; set; }
        public string BranchName { get; set; } = string.Empty;

        public SaleStatus Status { get; set; }
        public decimal TotalAmount { get; set; }

        public IEnumerable<ListSaleItemResponse> Items { get; set; } = [];
    }

    public class ListSaleItemResponse
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }

        public decimal Discount { get; set; }
        public SaleStatus Status { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
