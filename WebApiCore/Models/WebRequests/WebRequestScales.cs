// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WebApiCore.Enums;

namespace WebApiCore.Models.WebRequests;

public class WebRequestScales : WebRequestBase, IWebRequest
{
    #region Public and private methods

    public override List<string> GetListCore(ServerType serverType, string request) => serverType switch
    {
        ServerType.Product => new()
            {
                $"https://scales-prod-preview.kolbasa-vs.local:443/api/{request}",
                $"https://scales-prod.kolbasa-vs.local:443/api/{request}",
            },
        ServerType.Develop => new()
            {
                $"https://scales-dev-preview.kolbasa-vs.local:443/api/{request}",
                $"https://scales-dev.kolbasa-vs.local:443/api/{request}",
            },
        ServerType.All => new()
            {
                $"https://scales-dev-preview.kolbasa-vs.local:443/api/{request}",
                $"https://scales-dev.kolbasa-vs.local:443/api/{request}",
                $"https://scales-prod-preview.kolbasa-vs.local:443/api/{request}",
                $"https://scales-prod.kolbasa-vs.local:443/api/{request}",
            },
        _ => new(),
    };



    public List<string> GetListBarCodeBottom(ServerType serverType) => GetListCore(serverType, "get_barcode/bottom/");

    public List<string> GetListBarCodeBottomV1(ServerType serverType) => GetListCore(serverType, "v1/get_barcode/bottom/");

    public List<string> GetListBarCodeBottomV2(ServerType serverType) => GetListCore(serverType, "v2/get_barcode/bottom/");

    public List<string> GetListBarCodeBottomV3(ServerType serverType) => GetListCore(serverType, "v3/get_barcode/bottom/");


    public List<string> GetListBarCodeRight(ServerType serverType) => GetListCore(serverType, "get_barcode/right/");

    public List<string> GetListBarCodeRightV1(ServerType serverType) => GetListCore(serverType, "v1/get_barcode/right/");

    public List<string> GetListBarCodeRightV2(ServerType serverType) => GetListCore(serverType, "v2/get_barcode/right/");

    public List<string> GetListBarCodeRightV3(ServerType serverType) => GetListCore(serverType, "v3/get_barcode/right/");


    public List<string> GetListBarCodeTop(ServerType serverType) => GetListCore(serverType, "get_barcode/top/");

    public List<string> GetListBarCodeTopV1(ServerType serverType) => GetListCore(serverType, "v1/get_barcode/top/");

    public List<string> GetListBarCodeTopV2(ServerType serverType) => GetListCore(serverType, "v2/get_barcode/top/");

    public List<string> GetListBarCodeTopV3(ServerType serverType) => GetListCore(serverType, "v3/get_barcode/top/");

    #endregion
}
