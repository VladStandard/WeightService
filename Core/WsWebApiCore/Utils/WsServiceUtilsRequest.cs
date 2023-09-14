namespace WsWebApiCore.Utils;

/// <summary>
/// Утилиты веб-запросов.
/// </summary>
public static class WsServiceUtilsRequest
{
    #region Public and private methods
    
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