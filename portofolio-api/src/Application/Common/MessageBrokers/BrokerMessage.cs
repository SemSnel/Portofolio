using System.Text.Json;

namespace SemSnel.Portofolio.Application.Common.MessageBrokers;

public sealed class BrokerMessage
{
    public string Id { get; init; } = default!;
    public string Type { get; init; } = default!;
    public JsonElement Body { get; init; } = default!;
}