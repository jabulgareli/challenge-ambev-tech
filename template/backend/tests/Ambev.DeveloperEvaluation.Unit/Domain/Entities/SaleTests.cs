using Ambev.DeveloperEvaluation.Domain.Entities.Sales;
using Ambev.DeveloperEvaluation.Domain.Enums.Sales;
using Bogus;
using FluentAssertions;
using System;
using System.Linq;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities
{
    public class SaleTests
    {
        [Fact(DisplayName = "AggregateItems should group items by ProductId and recalculate total amount")]
        public void Given_ItemsWithSameProductId_When_Aggregated_Then_ShouldCombineQuantitiesAndAmounts()
        {
            // Arrange
            var productIdA = Guid.NewGuid();
            var sale = new Faker<Sale>()
                .RuleFor(s => s.Items, f => new[]
                {
                    new SaleItem
                    {
                        ProductId = productIdA,
                        ProductName = "Product A",
                        Quantity = 2,
                        UnitPrice = 10m,
                        Status = SaleItemStatus.Active
                    },
                    new SaleItem
                    {
                        ProductId = Guid.NewGuid(),
                        ProductName = "Product B",
                        Quantity = 3,
                        UnitPrice = 15m,
                        Status = SaleItemStatus.Active
                    },
                    new SaleItem
                    {
                        ProductId = productIdA,
                        ProductName = "Product A",
                        Quantity = 4,
                        UnitPrice = 10m,
                        Status = SaleItemStatus.Active
                    }
                })
                .Generate();

            // Act
            sale.AggregateItems();

            // Assert
            sale.Items.Should().HaveCount(2);
            sale.Items.Should().ContainSingle(i => i.ProductName == "Product A" && i.Quantity == 6 && i.TotalAmount == 60m);
            sale.Items.Should().ContainSingle(i => i.ProductName == "Product B" && i.Quantity == 3 && i.TotalAmount == 45m);
        }

        [Fact(DisplayName = "ApplyDiscounts should correctly apply discount tiers based on quantity")]
        public void Given_Items_When_ApplyDiscounts_Then_ShouldApplyCorrectDiscounts()
        {
            // Arrange
            var sale = new Faker<Sale>()
                .RuleFor(s => s.Items, f => new[]
                {
                    new SaleItem
                    {
                        ProductId = Guid.NewGuid(),
                        ProductName = "Product A",
                        Quantity = 3,
                        UnitPrice = 20m,
                        Status = SaleItemStatus.Active
                    },
                    new SaleItem
                    {
                        ProductId = Guid.NewGuid(),
                        ProductName = "Product B",
                        Quantity = 5,
                        UnitPrice = 15m,
                        Status = SaleItemStatus.Active
                    },
                    new SaleItem
                    {
                        ProductId = Guid.NewGuid(),
                        ProductName = "Product C",
                        Quantity = 12,
                        UnitPrice = 10m,
                        Status = SaleItemStatus.Active
                    }
                })
                .Generate();

            // Act
            sale.ApplyDiscounts();

            // Assert
            var items = sale.Items.ToList();
            items[0].Discount.Should().Be(0m);
            items[0].TotalAmount.Should().Be(60m);

            items[1].Discount.Should().Be(7.5m);
            items[1].TotalAmount.Should().Be(67.5m);

            items[2].Discount.Should().Be(24);
            items[2].TotalAmount.Should().Be(96m);
        }

        [Fact(DisplayName = "HasItemsWithInvalidQuantity should return true when there are invalid items")]
        public void Given_ItemsWithInvalidQuantities_When_Checked_Then_ShouldReturnTrue()
        {
            // Arrange
            var sale = new Faker<Sale>()
                .RuleFor(s => s.Items, f => new[]
                {
                    new SaleItem
                    {
                        ProductId = Guid.NewGuid(),
                        ProductName = "Product A",
                        Quantity = 25,
                        UnitPrice = 10m
                    },
                    new SaleItem
                    {
                        ProductId = Guid.NewGuid(),
                        ProductName = "Product B",
                        Quantity = 3,
                        UnitPrice = 15m
                    }
                })
                .Generate();

            // Act
            var result = sale.HasItemsWithInvalidQuantity();

            // Assert
            result.Should().BeTrue();
        }

        [Fact(DisplayName = "CancelItem should set item status to Cancelled")]
        public void Given_ExistingItem_When_Cancelled_Then_ShouldSetStatusToCancelled()
        {
            // Arrange
            var productId = Guid.NewGuid();
            var sale = new Faker<Sale>()
                .RuleFor(s => s.Items, f => new[]
                {
                    new SaleItem
                    {
                        ProductId = productId,
                        ProductName = "Product A",
                        Quantity = 2,
                        UnitPrice = 10m,
                        Status = SaleItemStatus.Active
                    },
                    new SaleItem
                    {
                        ProductId = Guid.NewGuid(),
                        ProductName = "Product B",
                        Quantity = 3,
                        UnitPrice = 15m,
                        Status = SaleItemStatus.Active
                    }
                })
                .Generate();

            // Act
            var result = sale.CancelItem(productId);

            // Assert
            result.Should().BeTrue();
            sale.Items.First(i => i.ProductId == productId).Status.Should().Be(SaleItemStatus.Cancelled);
        }

        [Fact(DisplayName = "CancelItem should return false if item does not exist")]
        public void Given_NonExistingItem_When_Cancelled_Then_ShouldReturnFalse()
        {
            // Arrange
            var sale = new Faker<Sale>()
                .RuleFor(s => s.Items, f => new[]
                {
                    new SaleItem
                    {
                        ProductId = Guid.NewGuid(),
                        ProductName = "Product A",
                        Quantity = 2,
                        UnitPrice = 10m,
                        Status = SaleItemStatus.Active
                    },
                    new SaleItem
                    {
                        ProductId = Guid.NewGuid(),
                        ProductName = "Product B",
                        Quantity = 3,
                        UnitPrice = 15m,
                        Status = SaleItemStatus.Active
                    }
                })
                .Generate();

            // Act
            var result = sale.CancelItem(Guid.NewGuid());

            // Assert
            result.Should().BeFalse();
        }

        [Fact(DisplayName = "Cancel should not change status if sale is already Cancelled")]
        public void Given_CancelledSale_When_CancelledAgain_Then_StatusShouldRemainCancelled()
        {
            // Arrange
            var sale = new Faker<Sale>()
                .RuleFor(s => s.Status, SaleStatus.Cancelled)
                .Generate();

            // Act
            sale.Cancel();

            // Assert
            sale.Status.Should().Be(SaleStatus.Cancelled);
        }

        [Fact(DisplayName = "RecalculateTotalAmount should exclude inactive items from total amount")]
        public void Given_ItemsWithInactiveStatus_When_Recalculated_Then_ShouldExcludeInactiveItems()
        {
            // Arrange
            var sale = new Faker<Sale>()
                .RuleFor(s => s.Items, f => new[]
                {
                    new SaleItem
                    {
                        ProductId = Guid.NewGuid(),
                        ProductName = "Product A",
                        Quantity = 2,
                        UnitPrice = 10m,
                        Status = SaleItemStatus.Cancelled
                    },
                    new SaleItem
                    {
                        ProductId = Guid.NewGuid(),
                        ProductName = "Product B",
                        Quantity = 3,
                        UnitPrice = 15m,
                        Status = SaleItemStatus.Active
                    }
                })
                .Generate();

            // Act
            sale.AggregateItems();
            sale.RecalculateTotalAmount();

            // Assert
            sale.TotalAmount.Should().Be(45m);
        }

        [Fact(DisplayName = "ApplyDiscounts should not fail for items with zero quantity")]
        public void Given_ItemsWithZeroQuantity_When_ApplyDiscounts_Then_DiscountShouldBeZero()
        {
            // Arrange
            var sale = new Faker<Sale>()
                .RuleFor(s => s.Items, f => new[]
                {
                    new SaleItem
                    {
                        ProductId = Guid.NewGuid(),
                        ProductName = "Product A",
                        Quantity = 0,
                        UnitPrice = 10m,
                        Status = SaleItemStatus.Active
                    }
                })
                .Generate();

            // Act
            sale.ApplyDiscounts();

            // Assert
            var item = sale.Items.First();
            item.Discount.Should().Be(0m);
            item.TotalAmount.Should().Be(0m);
        }

        [Fact(DisplayName = "AggregateItems should not alter unique items")]
        public void Given_UniqueItems_When_Aggregated_Then_ShouldRemainUnchanged()
        {
            // Arrange
            var sale = new Faker<Sale>()
                .RuleFor(s => s.Items, f => new[]
                {
                    new SaleItem
                    {
                        ProductId = Guid.NewGuid(),
                        ProductName = "Product A",
                        Quantity = 2,
                        UnitPrice = 10m,
                        Status = SaleItemStatus.Active
                    },
                    new SaleItem
                    {
                        ProductId = Guid.NewGuid(),
                        ProductName = "Product B",
                        Quantity = 3,
                        UnitPrice = 15m,
                        Status = SaleItemStatus.Active
                    }
                })
                .Generate();

            // Act
            sale.AggregateItems();

            // Assert
            sale.Items.Should().HaveCount(2);
            sale.Items.Sum(i => i.TotalAmount).Should().Be(65m);
        }
    }
}
