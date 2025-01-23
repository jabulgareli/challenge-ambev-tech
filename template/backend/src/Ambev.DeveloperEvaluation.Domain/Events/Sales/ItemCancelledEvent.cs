using MediatR;

namespace Ambev.DeveloperEvaluation.Domain.Events.Sales
{
    public class ItemCancelledEvent : INotification
    {
        public Guid SaleId { get; }
        public Guid ProductId { get; }

        public ItemCancelledEvent(Guid saleId, Guid productId)
        {
            SaleId = saleId;
            ProductId = productId;
        }
    }
}
