using Ambev.DeveloperEvaluation.Domain.Entities.Sales;
using Ambev.DeveloperEvaluation.Domain.Enums.Sales;
using Ambev.DeveloperEvaluation.Domain.Specifications.Sales;
using Bogus;
using FluentAssertions;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Specifications
{
    public class ItemAvailableSpecificationTests
    {
        [Theory]
        [InlineData(SaleItemStatus.Active, true)]
        [InlineData(SaleItemStatus.None, true)]
        [InlineData(SaleItemStatus.Cancelled, false)]
        public void IsSatisfiedBy_ShouldValidateItemAvailability(SaleItemStatus status, bool expectedResult)
        {
            // Arrange
            var saleItem = new Faker<SaleItem>()
                .RuleFor(i => i.Status, status)
                .Generate();

            var specification = new ItemAvailableSpecification();

            // Act
            var result = specification.IsSatisfiedBy(saleItem);

            // Assert
            result.Should().Be(expectedResult);
        }
    }

}
