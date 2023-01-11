// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsWebApiCore.Models.WebRequests;

public class WebRequestBase
{

    #region Public and private methods

    public virtual List<string> GetListCore(ServerType serverType, string request) => serverType switch
    {
        // (request.EndsWith('/') ? request : $"{request}/")
        ServerType.Product => new()
            {
                $"https://prod-preview.kolbasa-vs.local:443/api/{request}",
                $"https://prod.kolbasa-vs.local:443/api/{request}",
            },
        ServerType.Develop => new()
            {
                $"https://dev-preview.kolbasa-vs.local:443/api/{request}",
                $"https://dev.kolbasa-vs.local:443/api/{request}",
            },
        ServerType.All => new()
            {
                $"https://prod-preview.kolbasa-vs.local:443/api/{request}",
                $"https://prod.kolbasa-vs.local:443/api/{request}",
                $"https://dev-preview.kolbasa-vs.local:443/api/{request}",
                $"https://dev.kolbasa-vs.local:443/api/{request}",
            },
        _ => new(),
    };


    public List<string> GetListInfo(ServerType serverType) => GetListCore(serverType, "info/");

    public List<string> GetListSimple(ServerType serverType) => GetListCore(serverType, "simple/");

    public List<string> GetListException(ServerType serverType) => GetListCore(serverType, "exception/");

    public List<string> GetListWeatherForecast(ServerType serverType) => GetListCore(serverType, "weatherforecast/");

    public List<string> GetListWeatherForecastSpace(ServerType serverType) => GetListCore(serverType, "weather_forecast/");

    #endregion
}