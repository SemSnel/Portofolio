using System.Collections.ObjectModel;
using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;
using SemSnel.Portofolio.Domain._Common.Monads.ErrorOr;
using SemSnel.Portofolio.Server.Application.Common.Caching;

namespace SemSnel.Portofolio.Infrastructure.Common.Caching;

/// <summary>
/// A caching service.
/// </summary>
/// <param name="distributedCache"> The <see cref="IDistributedCache"/>. </param>
/// <param name="logger"> The <see cref="ILogger{TCategoryName}"/>. </param>
public class CachingService(
        IDistributedCache distributedCache,
        ILogger<CachingService> logger)
    : ICachingService
{
    private Collection<string> keys = new ();

    /// <inheritdoc/>
    public async Task<TResponse?> GetOrSetAsync<TResponse>(
        string key,
        Func<Task<TResponse>> factory,
        TimeSpan? timeToLive = null)
    {
        var jsonOptions = new JsonSerializerOptions
        {
            Converters =
            {
                new ErrorOrJsonConverterFactory(),
            },
        };

        var cachedResponse = await distributedCache.GetStringAsync(key);

        if (cachedResponse is not null)
        {
            var deserializedResponse = JsonSerializer.Deserialize<TResponse>(cachedResponse, jsonOptions);

            logger.LogInformation("Cache hit for key {Key}", key);
            logger.LogInformation("Returning cached response for key {Key}", key);

            return deserializedResponse;
        }

        var response = await factory();

        if (response is IErrorOr { IsError: true })
        {
            return response;
        }

        var serializedResponse = JsonSerializer.Serialize(response, jsonOptions);

        if (timeToLive is null)
        {
            await distributedCache.SetStringAsync(key, serializedResponse);

            return response;
        }

        logger.LogInformation("Cache miss for key {Key}", key);
        logger.LogInformation("Setting cache for key {Key} with time to live {TimeToLive}", key, timeToLive);

        await distributedCache.SetStringAsync(key, serializedResponse, new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = timeToLive,
        });

        keys.Add(key);

        return response;
    }
}