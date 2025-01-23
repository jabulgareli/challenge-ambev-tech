using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Enums.Sales;
using Ambev.DeveloperEvaluation.Domain.Specifications.Sales;
using Ambev.DeveloperEvaluation.Domain.Validation;

namespace Ambev.DeveloperEvaluation.Domain.Entities.Sales
{
    public class Sale : BaseEntity
    {
        private const int MinQuantityNoDiscount = 4;
        private const int MinQuantityTenPercentDiscount = 4;
        private const int MaxQuantityTenPercentDiscount = 9;
        private const int MinQuantityTwentyPercentDiscount = 10;
        private const int MaxQuantityTwentyPercentDiscount = 20;

        private const decimal TenPercentDiscount = 0.10m;
        private const decimal TwentyPercentDiscount = 0.20m;
        private const decimal NoDiscount = 0;

        public required string SaleNumber { get; init; }
        public DateTime SaleDate { get; init; }

        public required Guid CustomerId { get; init; }
        public required string CustomerName { get; init; }
        public required Guid BranchId { get; init; }
        public required string BranchName { get; init; }

        public decimal TotalAmount { get; private set; }
        public IEnumerable<SaleItem> Items { get; private set; } = [];

        public SaleStatus SaleStatus { get; init; }

        public ValidationResultDetail Validate()
        {
            var validator = new SaleValidator();
            var result = validator.Validate(this);
            return new ValidationResultDetail
            {
                IsValid = result.IsValid,
                Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
            };
        }


        public void AggregateItems()
        {
            var aggregatedItems = Items
                .GroupBy(item => item.ProductId)
                .Select(group =>
                {
                    var firstItem = group.First();
                    return new SaleItem
                    {
                        ProductId = firstItem.ProductId,
                        ProductName = firstItem.ProductName,
                        Quantity = group.Sum(item => item.Quantity),
                        UnitPrice = firstItem.UnitPrice,
                        Discount = 0,
                        TotalAmount = group.Sum(item => item.Quantity * item.UnitPrice)
                    };
                })
                .ToList();

            Items = aggregatedItems;

            RecalculateTotalAmount();
        }

        public void ApplyDiscounts()
        {
            foreach (var item in GetAvailableItems())
            {
                var discount = CalculateDiscountTier(item.Quantity);
                item.Discount = discount;
                item.TotalAmount = item.Quantity * item.UnitPrice * (1 - discount);
            }

            RecalculateTotalAmount();
        }

        private decimal CalculateDiscountTier(int quantity)
        {
            return quantity switch
            {
                < MinQuantityNoDiscount => NoDiscount,
                >= MinQuantityTenPercentDiscount and <= MaxQuantityTenPercentDiscount => TenPercentDiscount,
                >= MinQuantityTwentyPercentDiscount and <= MaxQuantityTwentyPercentDiscount => TwentyPercentDiscount,
                _ => NoDiscount
            };
        }

        public bool HasItemsWithInvalidQuantity()
        {
            var quantityLimitSpec = new QuantityLimitSpecification();
            return !GetAvailableItems().All(quantityLimitSpec.IsSatisfiedBy);
        }

        public void RecalculateTotalAmount()
        {
            TotalAmount = GetAvailableItems().Sum(item => item.TotalAmount);
        }

        public bool CancelItem(Guid productId)
        {
            var itemToRemove = Items.FirstOrDefault(item => item.ProductId == productId);

            if (itemToRemove is null)
                return false;

            return itemToRemove.Cancel();
        }

        public IEnumerable<SaleItem> GetAvailableItems() => Items.Where(i => i.Status == SaleItemStatus.Cancelled);
    }
}
