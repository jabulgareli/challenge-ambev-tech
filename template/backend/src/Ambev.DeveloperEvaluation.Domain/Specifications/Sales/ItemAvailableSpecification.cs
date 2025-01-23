using Ambev.DeveloperEvaluation.Domain.Entities.Sales;
using Ambev.DeveloperEvaluation.Domain.Specifications.Bases;

namespace Ambev.DeveloperEvaluation.Domain.Specifications.Sales
{
    public class ItemAvailableSpecification : Specification<SaleItem>
    {
        public override bool IsSatisfiedBy(SaleItem entity)
        {
            return entity.Status != Enums.Sales.SaleItemStatus.Cancelled;
        }
    }
}
