// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsWebApiCore.Models;

public class WsWebRequestTerra1000 : IWsWebRequest
{
    #region Public and private methods

    public List<string> GetListCore(WsServerType serverType, string request) => serverType switch
    {
        WsServerType.Product => new()
        {
            $"https://t1000-preview.kolbasa-vs.local:443/api/{request}",
            $"https://t1000.kolbasa-vs.local:443/api/{request}",
        },
        WsServerType.Develop => new()
        {
            $"https://t1000-dev-preview.kolbasa-vs.local:443/api/{request}",
            $"https://t1000-dev.kolbasa-vs.local:443/api/{request}",
        },
        WsServerType.All => new()
        {
            $"https://t1000-preview.kolbasa-vs.local:443/api/{request}",
            $"https://t1000.kolbasa-vs.local:443/api/{request}",
            $"https://t1000-dev-preview.kolbasa-vs.local:443/api/{request}",
            $"https://t1000-dev.kolbasa-vs.local:443/api/{request}",
        },
        _ => new(),
    };


    public List<string> GetListNomenclature(WsServerType serverType) => GetListCore(serverType, "nomenclature/");

    public List<string> GetListNomenclatureV2(WsServerType serverType) => GetListCore(serverType, "v2/nomenclature/");

    public List<string> GetListContragent(WsServerType serverType) => GetListCore(serverType, "contragent/");

    public List<string> GetListContragentV2(WsServerType serverType) => GetListCore(serverType, "v2/contragent/");

    #endregion
}