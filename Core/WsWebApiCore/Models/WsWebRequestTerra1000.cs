// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsWebApiCore.Models;

public class WsWebRequestTerra1000 : IWsWebRequest
{
    #region Public and private methods

    public List<string> GetListCore(WsSqlServerType serverType, string request) => serverType switch
    {
        WsSqlServerType.Product => new()
        {
            $"https://t1000-preview.kolbasa-vs.local:443/api/{request}",
            $"https://t1000.kolbasa-vs.local:443/api/{request}",
        },
        WsSqlServerType.Develop => new()
        {
            $"https://t1000-dev-preview.kolbasa-vs.local:443/api/{request}",
            $"https://t1000-dev.kolbasa-vs.local:443/api/{request}",
        },
        WsSqlServerType.All => new()
        {
            $"https://t1000-preview.kolbasa-vs.local:443/api/{request}",
            $"https://t1000.kolbasa-vs.local:443/api/{request}",
            $"https://t1000-dev-preview.kolbasa-vs.local:443/api/{request}",
            $"https://t1000-dev.kolbasa-vs.local:443/api/{request}",
        },
        _ => new(),
    };


    public List<string> GetListNomenclature(WsSqlServerType serverType) => GetListCore(serverType, "nomenclature/");

    public List<string> GetListNomenclatureV2(WsSqlServerType serverType) => GetListCore(serverType, "v2/nomenclature/");

    public List<string> GetListContragent(WsSqlServerType serverType) => GetListCore(serverType, "contragent/");

    public List<string> GetListContragentV2(WsSqlServerType serverType) => GetListCore(serverType, "v2/contragent/");

    #endregion
}