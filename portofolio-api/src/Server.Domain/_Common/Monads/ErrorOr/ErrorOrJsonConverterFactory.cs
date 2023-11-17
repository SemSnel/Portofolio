using System.Text.Json;
using System.Text.Json.Serialization;

namespace SemSnel.Portofolio.Domain._Common.Monads.ErrorOr;

/// <summary>
/// A Json converter factory for the <see cref="ErrorOr{T}"/> type.
/// </summary>
public class ErrorOrJsonConverterFactory : JsonConverterFactory
{
    /// <inheritdoc/>
    public override bool CanConvert(Type typeToConvert)
    {
        if (!typeToConvert.IsGenericType)
        {
            return false;
        }

        return typeToConvert.GetGenericTypeDefinition() == typeof(ErrorOr<>);
    }

    /// <inheritdoc/>
    public override JsonConverter CreateConverter(
        Type type,
        JsonSerializerOptions options)
    {
        var valueType = type.GetGenericArguments()[0];

        var converterType = typeof(ErrorOrJsonConverter<>)
            .MakeGenericType(new[] { valueType });

        var converter = (JsonConverter)Activator.CreateInstance(converterType)!;

        return converter ?? throw new NullReferenceException(nameof(converter));
    }
}