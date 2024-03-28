using Microsoft.Extensions.Caching.Distributed;
using Microsoft.IdentityModel.Tokens;
using PharmacyOnDuty.Aplication.Exceptions;
using PharmacyOnDuty.Application.Cache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace PharmacyOnDuty.Persistence.Cache
{
    public class RedisCacheService : ICacheService
    {
        private readonly IDistributedCache _cache;

        public RedisCacheService(IDistributedCache cache)
        {
            _cache = cache;
        }

        public Task ClearAll()
        {
            throw new NotImplementedException();
        }

        public async Task<T?> GetAsync<T>(string key)
        {
            var bytes = await _cache.GetAsync(key);
            if (bytes == null) return default(T?);

            var jsonStr = Encoding.UTF8.GetString(bytes);

            if (!jsonStr.IsNullOrEmpty())
            {
                return JsonSerializer.Deserialize<T>(jsonStr);
            }
            else
            {
                throw new RedisDataNotFoundException($"Data with key '{key}' not found in Redis.");
            }
        }

        public async Task SetAsync<T>(string key, T value, TimeSpan? expiry = null)
        {
            var serializedValue = JsonSerializer.Serialize(value);
            var utf8String = Encoding.UTF8.GetBytes(serializedValue);
            await _cache.SetAsync(key, utf8String, new()
            {
                AbsoluteExpirationRelativeToNow = expiry,
            });
        }
    }
}
