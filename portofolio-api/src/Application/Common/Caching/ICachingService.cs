namespace SemSnel.Portofolio.Application.Common.Caching;

public interface ICachingService
{
    public Task<TResponse?> GetOrSetAsync<TResponse>(string key,
        Func<Task<TResponse>> factory,
        TimeSpan? timeToLive = null);
}