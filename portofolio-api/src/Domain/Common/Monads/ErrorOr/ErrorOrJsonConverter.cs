using System.Text.Json;
using System.Text.Json.Serialization;

namespace SemSnel.Portofolio.Domain.Common.Monads.ErrorOr;

public class ErrorOrJsonConverter<TValue> : JsonConverter<ErrorOr<TValue>>
{
    public override ErrorOr<TValue?> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var jsonDocument = JsonDocument.ParseValue(ref reader);
        
        var rootElement = jsonDocument.RootElement;

        if (rootElement.TryGetProperty("Errors", out var errorsElement))
        {
            var errors = JsonSerializer.Deserialize<List<Error>>(errorsElement.GetRawText());

            return errors ?? new List<Error>();
        }

        var valueElement = rootElement.GetProperty("Value");

        var value = JsonSerializer.Deserialize<TValue>(valueElement.GetRawText());

        return value;
    }

    public override void Write(Utf8JsonWriter writer, ErrorOr<TValue> value, JsonSerializerOptions options)
    {
        if (value.IsError)
        {
            writer.WriteStartObject();
            writer.WritePropertyName("Errors");
            JsonSerializer.Serialize(writer, value.Errors);
            writer.WriteEndObject();
            return;
        }
        
        writer.WriteStartObject();
        writer.WritePropertyName("Value");
        JsonSerializer.Serialize(writer, value.Value);
        writer.WriteEndObject();
    }
}