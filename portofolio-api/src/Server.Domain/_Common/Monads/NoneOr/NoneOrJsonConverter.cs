using System.Text.Json;
using System.Text.Json.Serialization;

namespace SemSnel.Portofolio.Domain._Common.Monads.NoneOr;

/// <summary>
/// Json converter for the <see cref="NoneOr{T}"/> type.
/// </summary>
/// <typeparam name="T"> The type. </typeparam>
public class NoneOrJsonConverter<T> : JsonConverter<NoneOr<T>>
{
    /// <inheritdoc/>
    public override NoneOr<T> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.Null)
        {
            return NoneOr<T>.None();
        }

        var jsonDocument = JsonDocument.ParseValue(ref reader);

        var rootElement = jsonDocument.RootElement;

        var value = JsonSerializer.Deserialize<T>(rootElement.GetRawText());

        return NoneOr.Create(value);
    }

    /// <inheritdoc/>
    public override void Write(Utf8JsonWriter writer, NoneOr<T> value, JsonSerializerOptions options)
    {
        if (value.IsNone)
        {
            writer.WriteNullValue();

            return;
        }

        JsonSerializer.Serialize(writer, value.Value);
    }
}