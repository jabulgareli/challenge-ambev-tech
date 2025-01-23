using Ambev.DeveloperEvaluation.Domain.Entities.Sales;
using Ambev.DeveloperEvaluation.Domain.Specifications.Bases;

namespace Ambev.DeveloperEvaluation.Domain.Specifications.Sales
{
    public class ValidDiscountRangeSpecification : Specification<SaleItem>
    {
        public override bool IsSatisfiedBy(SaleItem item)
        {
            return item.Quantity >= 4 && item.Quantity <= 20;
        }
    }
}
