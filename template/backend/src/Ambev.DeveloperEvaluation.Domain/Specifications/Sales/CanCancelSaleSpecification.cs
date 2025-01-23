using Ambev.DeveloperEvaluation.Domain.Entities.Sales;
using Ambev.DeveloperEvaluation.Domain.Specifications.Bases;

namespace Ambev.DeveloperEvaluation.Domain.Specifications.Sales
{
    public class CanCancelSaleSpecification : Specification<Sale>
    {
        public override bool IsSatisfiedBy(Sale entity)
        {
            return entity.Status != Enums.Sales.SaleStatus.Cancelled;
        }
    }
}
