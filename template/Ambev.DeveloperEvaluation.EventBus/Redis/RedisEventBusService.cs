using Ambev.DeveloperEvaluation.Domain.Services;
using StackExchange.Redis;
using System.Text.Json;

namespace Ambev.DeveloperEvaluation.EventBus.Redis
{
    public class RedisEventBusService(IConnectionMultiplexer connectionMultiplexer) : IEventBusService
    {
        private readonly ISubscriber _subscriber = connectionMultiplexer.GetSubscriber(); 

        public async Task PublishAsync(string eventName, object message)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(eventName, nameof(eventName));

            var redisChannel = RedisChannel.Literal(eventName);
            var jsonData = JsonSerializer.Serialize(message);
            await _subscriber.PublishAsync(redisChannel, jsonData);
        }
    }
}
