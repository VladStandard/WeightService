// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsWebApi.Models.WebRequests;

internal interface IWsWebRequest
{
    List<string> GetListCore(ServerType serverType, string request);
}