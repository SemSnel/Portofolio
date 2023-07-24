using SemSnel.Portofolio.Application.Common.MessageBrokers;
using SemSnel.Portofolio.Application.WeatherForecasts.Features.Messages;
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
        var messages = new List<IMessage>();

        var fakeMessage = new WeatherForecastsCreatedMessage();
        
        messages.Add(fakeMessage);

        var errorOr = ErrorOr.From<IEnumerable<IMessage>>(messages);
        
        return errorOr;
    }

    public Task<ErrorOr<Deleted>> Delete(IMessage message)
    {
        return Task.FromResult(ErrorOr.From<Deleted>(new Deleted()));
    }
}