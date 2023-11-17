namespace SemSnel.Portofolio.Server.Application.Common.Caching;

/// <summary>
/// A request that can be cached.
/// </summary>
public interface ICacheableRequest
{
    /// <summary>
    /// Gets the cache key.
    /// </summary>
    /// <returns> The cache key. </returns>
    public string GetCacheKey();

    /// <summary>
    /// Gets the time to live.
    /// </summary>
    /// <returns> The time to live. </returns>
    public TimeSpan? GetTimeToLive();
}