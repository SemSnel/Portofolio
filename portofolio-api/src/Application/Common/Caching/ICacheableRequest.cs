namespace SemSnel.Portofolio.Application.Common.Caching;

public interface ICacheableRequest
{
    public string GetCacheKey();
    
    public TimeSpan? GetTimeToLive();
}