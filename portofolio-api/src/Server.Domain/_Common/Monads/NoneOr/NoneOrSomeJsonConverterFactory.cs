using System.Text.Json;
using System.Text.Json.Serialization;

namespace SemSnel.Portofolio.Domain._Common.Monads.NoneOr;

/// <summary>
/// A Json converter factory for the <see cref="NoneOr{T}"/> type.
/// </summary>
public sealed class NoneOrSomeJsonConverterFactory : JsonConverterFactory
{
    /// <inheritdoc/>
    public override bool CanConvert(Type typeToConvert)
    {
        if (!typeToConvert.IsGenericType)
        {
            return false;
        }

        var genericTypeDefinition = typeToConvert.GetGenericTypeDefinition();

        return genericTypeDefinition == typeof(NoneOr<>);
    }

    /// <inheritdoc/>
    public override JsonConverter? CreateConverter(Type typeToConvert, JsonSerializerOptions options)
    {
        var genericType = typeToConvert.GetGenericArguments()[0];

        var converterType = typeof(NoneOrJsonConverter<>).MakeGenericType(genericType);

        return (JsonConverter?)Activator.CreateInstance(converterType);
    }
}