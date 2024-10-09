using Refit;
using Ws.Shared.Utils;
using Ws.Shared.ValueTypes;

namespace Ws.Shared.Extensions;

public static class RefitExtensions
{
    public static string GetMessage(this ApiException ex, string fallbackMessage) =>
        StrUtils.TryDeserializeFromJson(ex.Content, out ApiFailedResponse? exception) ? exception.LocalizeMessage : fallbackMessage;
}