// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsWebApiCore.Utils;

public static class WebRequestUtils
{
    #region Public and private methods

    public static QueryParameter GetQueryParameterFormatJson() => new("format", "json");

    public static QueryParameter GetQueryParameterFormatXml() => new("format", "xml");

    public static RestRequest GetRequestFormatJson() => new RestRequest().AddQueryParameter("format", "json");

    public static RestRequest GetRequestFormatXml() => new RestRequest().AddQueryParameter("format", "xml");

    public static List<RestRequest> GetRequestFormats() => new() { GetRequestFormatJson(), GetRequestFormatXml() };

    public static RestRequest GetRequestCodeOrId(string? code, long? id)
    {
        RestRequest request = new();
        if (code is not null)
            request.AddQueryParameter("code", code);
        if (id is not null)
            request.AddQueryParameter("id", id.ToString());
        return request;
    }

    #endregion
}
