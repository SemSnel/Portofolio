using SemSnel.Portofolio.Domain.Common.Monads.ErrorOr;
using SemSnel.Portofolio.Domain.Common.Monads.Result;
using SemSnel.Portofolio.Infrastructure.Common.Idempotency.Entities;

namespace SemSnel.Portofolio.Infrastructure.Common.Idempotency;

public interface IIdempotencyService
{ 
    Task<ErrorOr<bool>> Exists(Guid id);
    
    Task<ErrorOr<Created>> SaveRequest(IdempotentRequest request);
    
    Task<ErrorOr<Success>> DeleteRequest(Guid id);
}