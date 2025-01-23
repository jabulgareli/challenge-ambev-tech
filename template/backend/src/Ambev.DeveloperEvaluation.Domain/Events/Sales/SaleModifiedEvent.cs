using MediatR;

namespace Ambev.DeveloperEvaluation.Domain.Events.Sales
{
    public class SaleModifiedEvent : INotification
    {
        public Guid SaleId { get; }
        public DateTime ModifiedDate { get; }

        public SaleModifiedEvent(Guid saleId, DateTime modifiedDate)
        {
            SaleId = saleId;
            ModifiedDate = modifiedDate;
        }
    }

}
