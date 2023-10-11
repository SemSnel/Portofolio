using SemSnel.Portofolio.Domain.Common.Monads.ErrorOr;
using SemSnel.Portofolio.Domain.Common.Monads.Result;
using SemSnel.Portofolio.Infrastructure.Common.MessageBrokers.Persistence.Entities;

namespace SemSnel.Portofolio.Infrastructure.Common.MessageBrokers.Persistence.Repositories;

public interface IOutBoxMessageRepository
{
    Task<ErrorOr<IEnumerable<OutBoxMessage>>> Get(string queueName, int take);
    
    ErrorOr<Success> Add(OutBoxMessage message);
    
    ErrorOr<Success> AddRange(IEnumerable<OutBoxMessage> messages);
    
    ErrorOr<Success> Update(OutBoxMessage message);
    
    ErrorOr<Success> UpdateRange(IEnumerable<OutBoxMessage> messages);
    
    ErrorOr<Success> Delete(Guid id);
    
    ErrorOr<Success> DeleteRange(IEnumerable<Guid> ids);
}