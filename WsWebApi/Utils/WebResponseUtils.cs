// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsWebApi.Utils;

public static class WebResponseUtils
{
    #region Public and private methods

    public static async Task GetResponseAsync(string url, RestRequest request, Action<RestResponse> action)
    {
        RestClientOptions options = new(url)
        {
            UseDefaultCredentials = true,
            ThrowOnAnyError = true,
            MaxTimeout = 60_000,
            RemoteCertificateValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
        };
        using RestClient client = new(options);
        RestResponse response = await client.GetAsync(request);

        action(response);
    }

    public static ResponseBarCodeModel CastBarCode(BarCodeModel barCode) =>
        new ResponseBarCodeModel().CloneCast(barCode);

    public static List<ResponseBarCodeModel> CastBarCodes(IEnumerable<BarCodeModel> barCodes) =>
        barCodes.Select(CastBarCode).ToList();

    #endregion
}