// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsWebApiCore.Utils;

internal static class WsWebXmlUtils
{
    public static XDocument? GetNullOrEmpty(string response)
    {
        XDocument? doc = null;
        if (string.IsNullOrEmpty(response))
        {
            doc = new(
                new XElement(WsWebConstants.Response,
                    new XElement(WsWebConstants.Error, new XAttribute(WsWebConstants.Description, "Result is null or empty!"))
                ));
        }
        return doc;
    }

    public static XDocument? GetError(string response)
    {
        XDocument? doc = null;
        if (response.Contains("<Error "))
        {
            WsSqlSimpleV1Model? error = JsonConvert.DeserializeObject<WsSqlSimpleV1Model>(response);
            doc = new(
                new XElement(WsWebConstants.Response,
                    new XElement(WsWebConstants.Error, new XAttribute(
                        WsWebConstants.Description, error is null ? string.Empty : error.Description))
                ));
        }
        return doc;
    }

    public static XDocument GetErrorUnknown() => new(
        new XElement(WsWebConstants.Response,
            new XElement(WsWebConstants.Error, new XAttribute(WsWebConstants.Description, "Unknown error!"))));
}