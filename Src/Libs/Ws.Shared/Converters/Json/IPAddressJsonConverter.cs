namespace Ws.Shared.Converters.Json;

public class IpAddressJsonConverter : JsonConverter<IPAddress>
{
    public override IPAddress Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        string? ipString = reader.GetString();
        if (IPAddress.TryParse(ipString, out IPAddress? ipAddress)) return ipAddress;
        throw new JsonException($"Invalid IP address format: {ipString}");
    }

    public override void Write(Utf8JsonWriter writer, IPAddress value, JsonSerializerOptions options) =>
        writer.WriteStringValue(value.ToString());
}