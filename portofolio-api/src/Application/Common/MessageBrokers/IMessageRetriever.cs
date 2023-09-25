using SemSnel.Portofolio.Domain.Common.Monads.ErrorOr;
using SemSnel.Portofolio.Domain.Common.Monads.Result;

namespace SemSnel.Portofolio.Application.Common.MessageBrokers;

/// <summary>
/// A service 
/// </summary>
public interface IMessageRetriever
{
    Task<ErrorOr<IEnumerable<IMessage>>> Get(string queueName, int take);

    Task<ErrorOr<Deleted>> Delete(IMessage message);
}