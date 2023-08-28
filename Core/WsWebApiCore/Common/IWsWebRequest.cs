namespace WsWebApiCore.Common;

internal interface IWsWebRequest
{
    List<string> GetListCore(WsSqlEnumServerType serverType, string request);
}