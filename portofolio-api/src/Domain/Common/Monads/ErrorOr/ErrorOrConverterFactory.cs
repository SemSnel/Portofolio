using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SemSnel.Portofolio.Domain.Common.Monads.ErrorOr;

public class ErrorOrConverterFactory : JsonConverterFactory
{
    public override bool CanConvert(Type typeToConvert)
    {
        if (!typeToConvert.IsGenericType)
        {
            return false;
        }

        return typeToConvert.GetGenericTypeDefinition() == typeof(ErrorOr<>);
    }

    public override JsonConverter CreateConverter(
        Type type,
        JsonSerializerOptions options)
    {
        var valueType = type.GetGenericArguments()[0];

        var converterType = typeof(ErrorOrJsonConverter<>)
            .MakeGenericType(new[] { valueType });
        
        var converter = (JsonConverter)Activator.CreateInstance(converterType);
        
        return converter ?? throw new NullReferenceException(nameof(converter));
    }
}