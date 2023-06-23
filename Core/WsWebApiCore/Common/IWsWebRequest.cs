// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsWebApiCore.Common;

internal interface IWsWebRequest
{
    List<string> GetListCore(WsSqlEnumServerType serverType, string request);
}