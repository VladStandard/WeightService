using Microsoft.Extensions.Localization;
using Ws.DeviceControl.Api.App.Shared.Localization;
using Ws.Shared.Resources;
// ReSharper disable ClassNeverInstantiated.Global

namespace Ws.DeviceControl.Api.App.Shared.Helpers;

public enum ErrorType
{
    [Description("errorUnique")]
    Unique,
    [Description("errorNotFound")]
    NotFound
}

public sealed class ErrorHelper(
    IStringLocalizer<ApplicationResources> localizer,
    IStringLocalizer<WsDataResources> wsDataLocalizer)
{
    public string Localize(ErrorType errorType, string fieldName = "")
    {
        string localizeErrorKey = errorType.GetDescription();
        return string.IsNullOrWhiteSpace(fieldName) ? localizer[localizeErrorKey] :
            string.Format(localizer[$"{localizeErrorKey}ByField"], wsDataLocalizer[$"Col{fieldName}"]);
    }
}