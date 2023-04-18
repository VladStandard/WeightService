// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsWebApiCore.Models;

public class WsWebRequestBase
{
    #region Public and private methods

    public virtual List<string> GetListCore(WsServerType serverType, string request) => serverType switch
    {
        // (request.EndsWith('/') ? request : $"{request}/")
        WsServerType.Product => new()
            {
                $"https://prod-preview.kolbasa-vs.local:443/api/{request}",
                $"https://prod.kolbasa-vs.local:443/api/{request}",
            },
        WsServerType.Develop => new()
            {
                $"https://dev-preview.kolbasa-vs.local:443/api/{request}",
                $"https://dev.kolbasa-vs.local:443/api/{request}",
            },
        WsServerType.All => new()
            {
                $"https://prod-preview.kolbasa-vs.local:443/api/{request}",
                $"https://prod.kolbasa-vs.local:443/api/{request}",
                $"https://dev-preview.kolbasa-vs.local:443/api/{request}",
                $"https://dev.kolbasa-vs.local:443/api/{request}",
            },
        _ => new(),
    };


    public List<string> GetListInfo(WsServerType serverType) => GetListCore(serverType, "info/");

    public List<string> GetListSimple(WsServerType serverType) => GetListCore(serverType, "simple/");

    public List<string> GetListException(WsServerType serverType) => GetListCore(serverType, "exception/");

    public List<string> GetListWeatherForecast(WsServerType serverType) => GetListCore(serverType, "weatherforecast/");

    public List<string> GetListWeatherForecastSpace(WsServerType serverType) => GetListCore(serverType, "weather_forecast/");

    #endregion
}