using WebApiCore.Enums;
// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WebApiCore.Models.WebRequests;

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

    public List<string> GetListInfoV1(ServerType serverType) => GetListCore(serverType, "v1/info/");

    public List<string> GetListInfoV2(ServerType serverType) => GetListCore(serverType, "v2/info/");

    public List<string> GetListInfoV3(ServerType serverType) => GetListCore(serverType, "v3/info/");

    
    public List<string> GetListSimple(ServerType serverType) => GetListCore(serverType, "simple/");
    
    public List<string> GetListSimpleV1(ServerType serverType) => GetListCore(serverType, "v1/simple/");
    
    public List<string> GetListSimpleV2(ServerType serverType) => GetListCore(serverType, "v2/simple/");
    
    public List<string> GetListSimpleV3(ServerType serverType) => GetListCore(serverType, "v3/simple/");

    
    public List<string> GetListException(ServerType serverType) => GetListCore(serverType, "exception/");
    
    public List<string> GetListExceptionV1(ServerType serverType) => GetListCore(serverType, "v1/exception/");
    
    public List<string> GetListExceptionV2(ServerType serverType) => GetListCore(serverType, "v2/exception/");
    
    public List<string> GetListExceptionV3(ServerType serverType) => GetListCore(serverType, "v3/exception/");

    
    public List<string> GetListWeatherForecast(ServerType serverType) => GetListCore(serverType, "weatherforecast/");
    
    public List<string> GetListWeatherForecastSpace(ServerType serverType) => GetListCore(serverType, "weather_forecast/");
    
    public List<string> GetListWeatherForecastV1(ServerType serverType) => GetListCore(serverType, "v1/weatherforecast/");
    
    public List<string> GetListWeatherForecastV2(ServerType serverType) => GetListCore(serverType, "v2/weatherforecast/");
    
    public List<string> GetListWeatherForecastV3(ServerType serverType) => GetListCore(serverType, "v3/weatherforecast/");

    #endregion
}