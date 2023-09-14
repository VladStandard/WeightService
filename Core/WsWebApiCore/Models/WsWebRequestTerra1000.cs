namespace WsWebApiCore.Models;

public sealed class WsWebRequestTerra1000
{
    #region Public and private methods

    public List<string> GetListCore(WsSqlEnumServerType serverType, string request) => serverType switch
    {
        WsSqlEnumServerType.Product => new()
        {
            $"https://t1000-preview.kolbasa-vs.local:443/api/{request}",
            $"https://t1000.kolbasa-vs.local:443/api/{request}",
        },
        WsSqlEnumServerType.Develop => new()
        {
            $"https://t1000-dev-preview.kolbasa-vs.local:443/api/{request}",
            $"https://t1000-dev.kolbasa-vs.local:443/api/{request}",
        },
        WsSqlEnumServerType.All => new()
        {
            $"https://t1000-preview.kolbasa-vs.local:443/api/{request}",
            $"https://t1000.kolbasa-vs.local:443/api/{request}",
            $"https://t1000-dev-preview.kolbasa-vs.local:443/api/{request}",
            $"https://t1000-dev.kolbasa-vs.local:443/api/{request}",
        },
        _ => new(),
    };

    public List<string> GetListNomenclature(WsSqlEnumServerType serverType) => GetListCore(serverType, "nomenclature/");

    public List<string> GetListNomenclatureV2(WsSqlEnumServerType serverType) => GetListCore(serverType, "v2/nomenclature/");

    public List<string> GetListContragent(WsSqlEnumServerType serverType) => GetListCore(serverType, "contragent/");

    public List<string> GetListContragentV2(WsSqlEnumServerType serverType) => GetListCore(serverType, "v2/contragent/");

    #endregion
}