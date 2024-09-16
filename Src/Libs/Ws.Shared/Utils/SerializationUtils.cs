using System.Text.Json;

namespace Ws.Shared.Utils;

public static class SerializationUtils
{
    public static bool TryDeserializeFromJson<T>(string json, out T? result)
    {
        try
        {
            result = JsonSerializer.Deserialize<T>(json);
            return result != null;
        }
        catch
        {
            result = default;
            return false;
        }
    }
}