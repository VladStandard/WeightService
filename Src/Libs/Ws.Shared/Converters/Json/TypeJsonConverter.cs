namespace Ws.Shared.Converters.Json;

public class TypeJsonConverter : JsonConverter<Type>
{
    public override Type Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        string? value = reader.GetString();

        if (string.IsNullOrWhiteSpace(value))
            throw new JsonException($"Invalid Type format: {value}");
        return Type.GetType(value) ?? throw new JsonException($"Invalid Type format: {value}");
    }

    public override void Write(Utf8JsonWriter writer, Type value, JsonSerializerOptions options) =>
        writer.WriteStringValue(value.AssemblyQualifiedName);
}