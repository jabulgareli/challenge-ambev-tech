using Ambev.DeveloperEvaluation.Domain.Services;
using StackExchange.Redis;
using System.Text.Json;

namespace Ambev.DeveloperEvaluation.Caching.Redis
{
    public class RedisCachingService(IConnectionMultiplexer connectionMultiplexer) : ICachingService
    {
        private readonly IDatabase _database = connectionMultiplexer.GetDatabase();

        public async Task SetAsync<T>(string key, T value, TimeSpan? expiration = null)
        {
            var jsonData = JsonSerializer.Serialize(value);
            await _database.StringSetAsync(key, jsonData, expiration);
        }

        public async Task<T?> GetAsync<T>(string key)
        {
            var jsonData = await _database.StringGetAsync(key);
            if (jsonData.IsNullOrEmpty) return default;
            return JsonSerializer.Deserialize<T>(jsonData!);
        }
    }
}
