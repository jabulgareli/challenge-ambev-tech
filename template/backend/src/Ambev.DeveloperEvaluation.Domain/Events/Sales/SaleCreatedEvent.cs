using MediatR;

namespace Ambev.DeveloperEvaluation.Domain.Events.Sales
{
    public class SaleCreatedEvent : INotification
    {
        public Guid Id { get; }
        public string SaleNumber { get; }

        public SaleCreatedEvent(Guid id, string saleNumber)
        {
            Id = id;
            SaleNumber = saleNumber;
        }
    }
}
