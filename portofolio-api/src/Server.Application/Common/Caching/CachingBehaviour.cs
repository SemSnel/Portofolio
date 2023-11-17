using SemSnel.Portofolio.Domain._Common.Monads.ErrorOr;

namespace SemSnel.Portofolio.Server.Application.Common.Caching;

/// <summary>
/// A caching behaviour.
/// When a request implements <see cref="ICacheableRequest"/>, the request will be cached.
/// </summary>
/// <typeparam name="TRequest"> The request type. </typeparam>
/// <typeparam name="TResponse"> The response type. </typeparam>
public class CachingBehaviour<TRequest, TResponse> :
    IPipelineBehavior<TRequest, TResponse>
    where TRequest : ICacheableRequest, IRequest<TResponse>
    where TResponse : IErrorOr
{
    private readonly ICachingService _cachingService;

    /// <summary>
    /// Initializes a new instance of the <see cref="CachingBehaviour{TRequest,TResponse}"/> class.
    /// </summary>
    /// <param name="cachingService"> The caching service. </param>
    public CachingBehaviour(ICachingService cachingService)
    {
        _cachingService = cachingService;
    }

    /// <summary>
    /// Handles the caching behaviour.
    /// </summary>
    /// <param name="request"> The request. </param>
    /// <param name="next"> The next request handler. </param>
    /// <param name="cancellationToken"> The cancellation token. </param>
    /// <returns> The response. </returns>
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        return await _cachingService.GetOrSetAsync(
            request.GetCacheKey(),
            async () => await next(),
            request.GetTimeToLive()) ?? throw new InvalidOperationException();
    }
}