namespace SemSnel.Portofolio.Server.Application.Common.Caching;

/// <summary>
/// A service to cache requests.
/// </summary>
public interface ICachingService
{
    /// <summary>
    /// Adds the value to the cache.
    /// </summary>
    /// <param name="key"> The cache key. </param>
    /// <param name="factory"> The factory. </param>
    /// <param name="timeToLive"> The time to live. </param>
    /// <typeparam name="TResponse"> The response type. </typeparam>
    /// <returns> The value. </returns>
    public Task<TResponse?> GetOrSetAsync<TResponse>(
        string key,
        Func<Task<TResponse>> factory,
        TimeSpan? timeToLive = null);
}