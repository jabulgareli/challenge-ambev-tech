namespace Ambev.DeveloperEvaluation.Domain.Services
{
    public interface IEventBusService
    {
        Task PublishAsync(string eventName, object @event);
    }
}
