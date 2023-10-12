using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;
using SemSnel.Portofolio.Application.Common.Caching;
using SemSnel.Portofolio.Domain.Common.Monads.ErrorOr;

namespace SemSnel.Portofolio.Infrastructure.Common.Caching;

public class CachingService : ICachingService
{
    private readonly IDistributedCache _distributedCache;

    public CachingService(IDistributedCache distributedCache)
    {
        _distributedCache = distributedCache;
    }

    public async Task<TResponse?> GetOrSetAsync<TResponse>(
        string key,
        Func<Task<TResponse>> factory,
        TimeSpan? timeToLive = null)
    {
        var jsonOptions = new JsonSerializerOptions
        {
            Converters =
            {
                new ErrorOrConverterFactory()
            }
        };
        
        var cachedResponse = await _distributedCache.GetStringAsync(key);

        if (cachedResponse != null)
        {
            var deserializedResponse = JsonSerializer.Deserialize<TResponse>(cachedResponse, jsonOptions);
            
            return deserializedResponse;
        }

        var response = await factory();

        if (response is IErrorOr { IsError: true })
        {
            return response;
        }
        
        var serializedResponse = JsonSerializer.Serialize(response, jsonOptions);

        if (timeToLive != null)
        {
            await _distributedCache.SetStringAsync(key, serializedResponse, new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = timeToLive
            });
        }
        else
        {
            await _distributedCache.SetStringAsync(key, serializedResponse);
        }

        return response;
    }
}