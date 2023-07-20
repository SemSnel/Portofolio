using SemSnel.Portofolio.Application.Common.MessageBrokers;
using SemSnel.Portofolio.Domain.Common.Monads.ErrorOr;
using SemSnel.Portofolio.Domain.Common.Monads.Result;

namespace SemSnel.Portofolio.Infrastructure.Common.MessageBrokers;

public class MessageService : IMessageService
{
    public Task<ErrorOr<Success>> Send(IMessage message)
    {
        throw new NotImplementedException();
    }

    public async Task<ErrorOr<IEnumerable<IMessage>>> Get(string queueName, int take)
    {
        var messages = Enumerable.Empty<IMessage>();

        var errorOr = ErrorOr.From(messages);
        
        return errorOr;
    }

    public Task<ErrorOr<Deleted>> Delete(IMessage message)
    {
        throw new NotImplementedException();
    }
}