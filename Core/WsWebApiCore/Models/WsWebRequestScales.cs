// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsWebApiCore.Models;

public class WsWebRequestScales : WsWebRequestBase, IWsWebRequest
{
    #region Public and private methods

    public override List<string> GetListCore(WsServerType serverType, string request) => serverType switch
    {
        WsServerType.Product => new()
            {
                $"https://scales-prod-preview.kolbasa-vs.local:443/api/{request}",
                $"https://scales-prod.kolbasa-vs.local:443/api/{request}",
            },
        WsServerType.Develop => new()
            {
                $"https://scales-dev-preview.kolbasa-vs.local:443/api/{request}",
                $"https://scales-dev.kolbasa-vs.local:443/api/{request}",
            },
        WsServerType.All => new()
            {
                $"https://scales-dev-preview.kolbasa-vs.local:443/api/{request}",
                $"https://scales-dev.kolbasa-vs.local:443/api/{request}",
                $"https://scales-prod-preview.kolbasa-vs.local:443/api/{request}",
                $"https://scales-prod.kolbasa-vs.local:443/api/{request}",
            },
        _ => new(),
    };



    public List<string> GetListBarCodeBottom(WsServerType serverType) => GetListCore(serverType, "get_barcode/bottom/");

    public List<string> GetListBarCodeBottomV1(WsServerType serverType) => GetListCore(serverType, "v1/get_barcode/bottom/");

    public List<string> GetListBarCodeBottomV2(WsServerType serverType) => GetListCore(serverType, "v2/get_barcode/bottom/");

    public List<string> GetListBarCodeBottomV3(WsServerType serverType) => GetListCore(serverType, "v3/get_barcode/bottom/");


    public List<string> GetListBarCodeRight(WsServerType serverType) => GetListCore(serverType, "get_barcode/right/");

    public List<string> GetListBarCodeRightV1(WsServerType serverType) => GetListCore(serverType, "v1/get_barcode/right/");

    public List<string> GetListBarCodeRightV2(WsServerType serverType) => GetListCore(serverType, "v2/get_barcode/right/");

    public List<string> GetListBarCodeRightV3(WsServerType serverType) => GetListCore(serverType, "v3/get_barcode/right/");


    public List<string> GetListBarCodeTop(WsServerType serverType) => GetListCore(serverType, "get_barcode/top/");

    public List<string> GetListBarCodeTopV1(WsServerType serverType) => GetListCore(serverType, "v1/get_barcode/top/");

    public List<string> GetListBarCodeTopV2(WsServerType serverType) => GetListCore(serverType, "v2/get_barcode/top/");

    public List<string> GetListBarCodeTopV3(WsServerType serverType) => GetListCore(serverType, "v3/get_barcode/top/");

    #endregion
}