using SemSnel.Portofolio.Domain.Common.Monads.ErrorOr;

namespace SemSnel.Portofolio.Application.Common.MessageBrokers;

public readonly record struct ReceiveMessagesRequest
    (int MaxAmountOfMessages = 10) : IRequest<ErrorOr<ReceiveMessagesResponse>>;

public sealed class ReceiveMessagesResponse
{
    public IEnumerable<IMessage> Messages { get; set; } = Enumerable.Empty<IMessage>();
}

public sealed class ReceiveMessagesRequestHandler : IRequestHandler<ReceiveMessagesRequest, ErrorOr<ReceiveMessagesResponse>>
{
    private readonly IMessageService _messageService;

    public ReceiveMessagesRequestHandler(IMessageService messageService)
    {
        _messageService = messageService;
    }

    public async Task<ErrorOr<ReceiveMessagesResponse>> Handle(ReceiveMessagesRequest request, CancellationToken cancellationToken)
    {
        var errorOr = await _messageService.Get("", 10);

        if (errorOr.IsError)
        {
            return errorOr.FirstError;
        }

        var messages = errorOr.Value;
        
        return new ReceiveMessagesResponse()
        {
            Messages = messages
        };
    }
}

