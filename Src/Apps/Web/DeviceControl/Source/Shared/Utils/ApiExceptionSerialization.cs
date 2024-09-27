using Refit;
using Ws.Shared.Api.ApiException;

namespace DeviceControl.Source.Shared.Utils;

public static class ApiExceptionSerialization
{
    public static string GetMessage(ApiException ex, string fallbackMessage = "") =>
        StrUtils.TryDeserializeFromJson(ex.Content, out ApiExceptionClient? exception) ? exception.LocalizeMessage : fallbackMessage;
}