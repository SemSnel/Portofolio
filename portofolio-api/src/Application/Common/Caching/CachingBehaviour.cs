using SemSnel.Portofolio.Domain.Common.Monads.ErrorOr;

namespace SemSnel.Portofolio.Application.Common.Caching;

public class CachingBehaviour<TRequest, TResponse> : 
    IPipelineBehavior<TRequest, TResponse>
    where TRequest : ICacheableRequest, IRequest<TResponse>
    where TResponse : IErrorOr
{
    private readonly ICachingService _cachingService;

    public CachingBehaviour(ICachingService cachingService)
    {
        _cachingService = cachingService;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        return await _cachingService.GetOrSetAsync(
            request.GetCacheKey(), 
             async () => await next(),
            request.GetTimeToLive())!;
    }
}