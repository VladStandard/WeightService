using Refit;
using Ws.Shared.Web.ValueTypes;

namespace Ws.Shared.Web.Extensions;

public static class RefitExtensions
{
    [Pure]
    public static string GetMessage(this ApiException ex, string fallbackMessage) =>
        StrUtils.TryDeserializeFromJson(ex.Content, out ApiFailedResponse? exception) ? exception.LocalizeMessage : fallbackMessage;
}