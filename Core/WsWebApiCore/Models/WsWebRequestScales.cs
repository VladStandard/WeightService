// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsWebApiCore.Models;

public class WsWebRequestScales : IWsWebRequest
{
    #region Public and private methods

    public List<string> GetListCore(WsSqlServerType serverType, string request) => serverType switch
    {
        WsSqlServerType.Product => new()
        {
            $"https://scales-prod-preview.kolbasa-vs.local:443/api/{request}",
            $"https://scales-prod.kolbasa-vs.local:443/api/{request}",
        },
        WsSqlServerType.Develop => new()
        {
            $"https://scales-dev-preview.kolbasa-vs.local:443/api/{request}",
            $"https://scales-dev.kolbasa-vs.local:443/api/{request}",
        },
        WsSqlServerType.All => new()
        {
            $"https://scales-dev-preview.kolbasa-vs.local:443/api/{request}",
            $"https://scales-dev.kolbasa-vs.local:443/api/{request}",
            $"https://scales-prod-preview.kolbasa-vs.local:443/api/{request}",
            $"https://scales-prod.kolbasa-vs.local:443/api/{request}",
        },
        _ => new(),
    };

    #endregion
}