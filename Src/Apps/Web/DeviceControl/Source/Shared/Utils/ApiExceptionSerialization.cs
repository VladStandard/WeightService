using Refit;
using Ws.Shared.Api.ApiException;

namespace DeviceControl.Source.Shared.Utils;

public static class ApiExceptionSerialization
{
    public static string GetMessage(ApiException ex, string fallbackMessage = "") =>
        ex.HasContent &&
        StrUtils.TryDeserializeFromJson(ex.Content ?? string.Empty, out ApiExceptionClient? exception)
            ? exception.LocalizeMessage : fallbackMessage;
}