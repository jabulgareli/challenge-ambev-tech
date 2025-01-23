using MediatR;

namespace Ambev.DeveloperEvaluation.Domain.Events.Sales
{
    public class SaleCancelledEvent : INotification
    {
        public Guid SaleId { get; }

        public SaleCancelledEvent(Guid saleId)
        {
            SaleId = saleId;
        }
    }
}
