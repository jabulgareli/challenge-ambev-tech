﻿using Ambev.DeveloperEvaluation.Domain.Enums.Sales;

namespace Ambev.DeveloperEvaluation.Domain.Entities.Sales
{
    public class SaleItem
    {
        public required Guid ProductId { get; init; }
        public required string ProductName { get; init; }
        public int Quantity { get; init; }
        public decimal UnitPrice { get; init; }
        public decimal Discount { get; internal set; }
        public decimal TotalAmount { get; internal set; }
        public SaleItemStatus Status { get; set; } = SaleItemStatus.Active;

        internal bool Cancel()
        {
            if (Status != SaleItemStatus.Active)
                return false;

            Status = SaleItemStatus.Cancelled;
            return true;
        }
    }
}
