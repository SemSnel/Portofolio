using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SemSnel.Portofolio.Domain._Common.Monads.ErrorOr;
using SemSnel.Portofolio.Domain._Common.Monads.NoneOr;
using SemSnel.Portofolio.Domain._Common.Monads.Result;
using SemSnel.Portofolio.Infrastructure.Common.Idempotency.Entities;
using SemSnel.Portofolio.Server.Application.Common.DateTime;

namespace SemSnel.Portofolio.Infrastructure.Common.Idempotency;

/// <summary>
/// An idempotency service.
/// </summary>
/// <param name="dbContext"> The <see cref="IAppDatabaseContext"/>. </param>
/// <param name="dateTimeProvider"> The <see cref="IDateTimeProvider"/>. </param>
/// <param name="idempotencySettings"> The <see cref="IOptions{TOptions}"/>. </param>
public sealed class IdempotencyService(
        IAppDatabaseContext dbContext,
        IDateTimeProvider dateTimeProvider,
        IOptions<IdempotencySettings> idempotencySettings)
    : IIdempotencyService
{
    private readonly IdempotencySettings _idempotencySettings = idempotencySettings.Value;

    /// <inheritdoc/>
    public async Task<ErrorOr<bool>> Exists(Guid id)
    {
        var request = await dbContext
            .IdempotentRequests
            .FirstOrDefaultAsync(x => x.Id == id);

        if (request is null)
        {
            return false;
        }

        var now = dateTimeProvider.Now();
        var expirationDate = GetExpirationDate(request.CreatedOn);

        if (now <= expirationDate)
        {
            return true;
        }

        dbContext.IdempotentRequests.Remove(request);

        return false;
    }

    /// <inheritdoc/>
    public async Task<ErrorOr<Created>> SaveRequest(IdempotentRequest request)
    {
        await dbContext
            .IdempotentRequests
            .AddAsync(request);

        var result = await dbContext.SaveChangesAsync();

        return Result.Created;
    }

    /// <inheritdoc/>
    public async Task<ErrorOr<Success>> DeleteRequest(Guid id)
    {
        var request = dbContext.IdempotentRequests.FirstOrDefault(x => x.Id == id);

        if (request is null)
        {
            return Error.NotFound("Request not found");
        }

        dbContext.IdempotentRequests.Remove(request);

        var result = await dbContext.SaveChangesAsync();

        if (result > 0)
        {
            return Result.Success;
        }

        return Error.Failure("Failed to delete request");
    }

    /// <inheritdoc/>
    public async Task<ErrorOr<NoneOr<IdempotentRequest>>> GetRequest(Guid idempotencyKey)
    {
        var request = await dbContext
            .IdempotentRequests
            .FirstOrDefaultAsync(x => x.Id == idempotencyKey);

        if (request is null)
        {
            return NoneOr.None<IdempotentRequest>();
        }

        var now = dateTimeProvider.Now();
        var expirationDate = GetExpirationDate(request.CreatedOn);

        if (now <= expirationDate)
        {
            return request.ToNoneOrSome();
        }

        dbContext.IdempotentRequests.Remove(request);

        return Error.NotFound("Request not found");
    }

    private System.DateTime GetExpirationDate(System.DateTime dateTime)
    {
        var interval = _idempotencySettings.ExpirationInterval;
        var intervalType = _idempotencySettings.ExpirationIntervalUnit;

        return intervalType switch
        {
            "seconds" => dateTime.AddSeconds(interval),
            "minutes" => dateTime.AddMinutes(interval),
            "hours" => dateTime.AddHours(interval),
            "days" => dateTime.AddDays(interval),
            _ => throw new ArgumentException($"Invalid interval type {intervalType}")
        };
    }
}