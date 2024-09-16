using Refit;
using Ws.Shared.Api.ApiException;

namespace DeviceControl.Source.Shared.Utils;

public static class ApiExceptionSerialization
{
    public static string GetMessage(ApiException ex, string fallbackMessage = "") =>
        ex.HasContent &&
        !string.IsNullOrEmpty(ex.Content) &&
        SerializationUtils.TryDeserializeFromJson(ex.Content, out ApiExceptionClient? exception) &&
        exception != null
            ? exception.LocalizeMessage
            : fallbackMessage;
}