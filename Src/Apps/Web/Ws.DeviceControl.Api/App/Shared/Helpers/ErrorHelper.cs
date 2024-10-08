using Microsoft.Extensions.Localization;
using Ws.DeviceControl.Api.App.Shared.Localization;
using Ws.Shared.Resources;

namespace Ws.DeviceControl.Api.App.Shared.Helpers;

public sealed class ErrorHelper(
    IStringLocalizer<ApplicationResources> localizer,
    IStringLocalizer<WsDataResources> wsDataLocalizer)
{
    public string Localize(ErrorType errorType, string fieldName = "")
    {
        string localizeErrorKey = errorType switch
        {
            ErrorType.Unique => "errorUnique",
            ErrorType.NotFound => "errorNotFound",
            _ => "errorUnknown"
        };
        return string.IsNullOrWhiteSpace(fieldName) ? localizer[localizeErrorKey] :
            string.Format(localizer[$"{localizeErrorKey}ByField"], wsDataLocalizer[$"Col{fieldName}"]);
    }
}

public enum ErrorType
{
    Unique,
    NotFound,
    Unknown
}