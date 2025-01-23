using Ambev.DeveloperEvaluation.Domain.Entities.Sales;
using Ambev.DeveloperEvaluation.Domain.Specifications.Sales;
using Bogus;
using FluentAssertions;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Specifications
{
    public class QuantityLimitSpecificationTests
    {
        [Theory]
        [InlineData(5, true)]
        [InlineData(20, true)]
        [InlineData(25, false)]
        public void IsSatisfiedBy_ShouldValidateItemQuantity(int quantity, bool expectedResult)
        {
            // Arrange
            var saleItem = new Faker<SaleItem>()
                .RuleFor(i => i.Quantity, quantity)
                .Generate();

            var specification = new QuantityLimitSpecification();

            // Act
            var result = specification.IsSatisfiedBy(saleItem);

            // Assert
            result.Should().Be(expectedResult);
        }
    }
}
