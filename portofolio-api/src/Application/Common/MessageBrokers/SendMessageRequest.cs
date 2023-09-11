using SemSnel.Portofolio.Domain.Common.Monads.ErrorOr;
using SemSnel.Portofolio.Domain.Common.Monads.Result;

namespace SemSnel.Portofolio.Application.Common.MessageBrokers;

public sealed class SendMessageRequest : IRequest<ErrorOr<Success>>
{
    public IMessage Message { get; init; } = default!;
}

public sealed class SendMessageRequestHandler : IRequestHandler<SendMessageRequest, ErrorOr<Success>>
{
    public Task<ErrorOr<Success>> Handle(SendMessageRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}