using Microsoft.Extensions.Localization;
using Ws.DeviceControl.Api.App.Shared.Localization;
using Ws.Shared.Resources;

namespace Ws.DeviceControl.Api.App.Shared;

public class ErrorMessages(
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
        return string.IsNullOrEmpty(fieldName) ? localizer[localizeErrorKey] :
            string.Format(localizer[$"{localizeErrorKey}ByField"], wsDataLocalizer[$"Col{fieldName}"]);
    }
}

public enum ErrorType
{
    Unique,
    NotFound,
    Unknown
}