// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WebApiCore.Enums;

namespace WebApiCore.Models.WebRequests;

internal interface IWebRequest
{
    List<string> GetListCore(ServerType serverType, string request);
}
