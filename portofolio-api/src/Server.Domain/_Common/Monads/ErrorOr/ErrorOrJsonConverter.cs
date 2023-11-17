using System.Text.Json;
using System.Text.Json.Serialization;

namespace SemSnel.Portofolio.Domain._Common.Monads.ErrorOr;

/// <summary>
/// The error or json converter.
/// </summary>
/// <typeparam name="TValue">The value type.</typeparam>
public class ErrorOrJsonConverter<TValue> : JsonConverter<ErrorOr<TValue>>
{
    /// <summary>
    /// Reads the error or from json.
    /// </summary>
    /// <param name="reader"> The json reader. </param>
    /// <param name="typeToConvert"> The type to convert. </param>
    /// <param name="options"> The json serializer options. </param>
    /// <returns> The error or. </returns>
    public override ErrorOr<TValue> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
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

        return value!;
    }

    /// <summary>
    /// Writes the error or to json.
    /// </summary>
    /// <param name="writer"> The json writer. </param>
    /// <param name="value"> The error or. </param>
    /// <param name="options"> The json serializer options. </param>
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