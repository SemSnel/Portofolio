using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SemSnel.Portofolio.Application.Common.DateTime;
using SemSnel.Portofolio.Domain.Common.Monads.ErrorOr;
using SemSnel.Portofolio.Domain.Common.Monads.Result;
using SemSnel.Portofolio.Infrastructure.Common.Idempotency.Entities;
using SemSnel.Portofolio.Infrastructure.Common.Persistence.Database;

namespace SemSnel.Portofolio.Infrastructure.Common.Idempotency;

public sealed class IdempotencyService : IIdempotencyService
{
    private readonly IAppDatabaseContext _dbContext;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly IdempotencySettings _idempotencySettings;

    public IdempotencyService(IAppDatabaseContext dbContext, IDateTimeProvider dateTimeProvider, IOptions<IdempotencySettings> idempotencySettings)
    {
        _dbContext = dbContext;
        _dateTimeProvider = dateTimeProvider;
        _idempotencySettings = idempotencySettings.Value;
    }

    public async Task<ErrorOr<bool>> Exists(Guid id)
    {
        // get the request from the database and check if it exists and the request is not expired
        
        var request = await _dbContext
            .IdempotentRequests
            .FirstOrDefaultAsync(x => x.Id == id);
        
        if (request is null)
        {
            return false;
        }

        var now = _dateTimeProvider.Now();
        var expirationDate = GetExpirationDate(request.CreatedOn);

        if (now <= expirationDate)
        {
            return true;
        }
        
        _dbContext.IdempotentRequests.Remove(request);
            
        return false;

    }

    public async Task<ErrorOr<Created>> SaveRequest(IdempotentRequest request)
    {
        await _dbContext
            .IdempotentRequests
            .AddAsync(request);
        
        return Result.Created();
    }

    public async Task<ErrorOr<Success>> DeleteRequest(Guid id)
    {
        var request = _dbContext.IdempotentRequests.FirstOrDefault(x => x.Id == id);
        
        if (request is null)
        {
            return Error.NotFound("Request not found");
        }
        
        _dbContext.IdempotentRequests.Remove(request);
        
        var result = await _dbContext.SaveChangesAsync();
        
        if (result > 0)
        {
            return Result.Success();
        }
        
        return Error.Failure("Failed to delete request");
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