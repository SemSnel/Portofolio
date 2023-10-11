using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SemSnel.Portofolio.Domain.Common.Monads.ErrorOr;
using SemSnel.Portofolio.Domain.Common.Monads.Result;
using SemSnel.Portofolio.Infrastructure.Common.MessageBrokers.Persistence.Entities;
using SemSnel.Portofolio.Infrastructure.Common.Persistence.Database;

namespace SemSnel.Portofolio.Infrastructure.Common.MessageBrokers.Persistence.Repositories;

public class OutBoxMessageRepository : IOutBoxMessageRepository
{
    private readonly IAppDatabaseContext _context;

    public OutBoxMessageRepository(IAppDatabaseContext context)
    {
        _context = context;
    }

    public async Task<ErrorOr<IEnumerable<OutBoxMessage>>> Get(string queueName, int take)
    {
        return await _context.OutboxMessages
            .AsQueryable()
            .Where(message => message.ProcessedOn == null)
            .OrderBy(message => message.CreatedOn)
            .Take(take)
            .ToListAsync();
    }
    
    public ErrorOr<Success> Add(OutBoxMessage message)
    {
        _context.OutboxMessages.Add(message);
        
        return Result.Success();
    }
    
    public ErrorOr<Success> AddRange(IEnumerable<OutBoxMessage> messages)
    {
        _context.OutboxMessages.AddRange(messages);
        
        return Result.Success();
    }
    
    public ErrorOr<Success> Update(OutBoxMessage message)
    {
        _context.OutboxMessages.Update(message);
        
        return Result.Success();
    }
    
    public ErrorOr<Success> UpdateRange(IEnumerable<OutBoxMessage> messages)
    {
        _context.OutboxMessages.UpdateRange(messages);
        
        return Result.Success();
    }
    
    public ErrorOr<Success> Delete(Guid id)
    {
        var message = _context.OutboxMessages.Find(id);
        
        if (message is null)
        {
            return Error.NotFound();
        }
        
        _context.OutboxMessages.Remove(message);
        
        return Result.Success();
    }
    
    public ErrorOr<Success> DeleteRange(IEnumerable<Guid> ids)
    {
        var messages = _context.OutboxMessages.Where(message => ids.Contains(message.Id));
        
        if (!messages.Any())
        {
            return Error.NotFound();
        }
        
        _context.OutboxMessages.RemoveRange(messages);
        
        return Result.Success();
    }
}