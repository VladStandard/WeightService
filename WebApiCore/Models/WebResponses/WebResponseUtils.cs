// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Net;
using DataCore.Sql.TableScaleModels.BarCodes;
using Newtonsoft.Json;
using NUnit.Framework;
using RestSharp;
using WebApiCore.Models.WebRequests;

namespace WebApiCore.Models.WebResponses;

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

    public static async Task GetInfoAsync(string url, RestRequest request)
    {
        await GetResponseAsync(url, request, response =>
        {
            TestContext.WriteLine($"{nameof(response.ResponseUri)}: {response.ResponseUri}");
            Assert.IsNotEmpty(response.Content);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);

            if (!string.IsNullOrEmpty(response.Content))
            {
                ServiceInfoModel? serviceInfo = null;
                if (request.Parameters.Contains(WebRequestUtils.GetQueryParameterFormatJson()))
                {
                    serviceInfo = JsonConvert.DeserializeObject<ServiceInfoModel>(response.Content);
                    Assert.IsTrue(serviceInfo is not null);
                }
                else if (request.Parameters.Contains(WebRequestUtils.GetQueryParameterFormatXml()))
                {
                    serviceInfo = new ServiceInfoModel().DeserializeFromXml<ServiceInfoModel>(response.Content);
                    Assert.IsTrue(serviceInfo is not null);
                }
                if (serviceInfo is not null)
                {
                    Assert.IsNotEmpty(serviceInfo.App);
                    Assert.IsTrue(serviceInfo.App.StartsWith("WebApi", StringComparison.InvariantCultureIgnoreCase));
                    Assert.IsNotEmpty(serviceInfo.App);
                    Assert.IsNotEmpty(serviceInfo.Version);
                    Assert.IsNotEmpty(serviceInfo.WinCurrentDate);
                    Assert.IsNotEmpty(serviceInfo.SqlCurrentDate);
                    Assert.IsNotEmpty(serviceInfo.ConnectionString);
                    Assert.Greater(serviceInfo.ConnectTimeout, 0);
                    Assert.IsNotEmpty(serviceInfo.DataSource);
                    Assert.IsNotEmpty(serviceInfo.ServerVersion);
                    Assert.IsNotEmpty(serviceInfo.Database);
                    Assert.Greater(serviceInfo.PhysicalMegaBytes, 0);
                    Assert.Greater(serviceInfo.VirtualMegaBytes, 0);
                }
            }
        });
    }
    
    public static async Task GetExceptionAsync(string url, RestRequest request)
    {
        await GetResponseAsync(url, request, response =>
        {
            TestContext.WriteLine($"{nameof(response.ResponseUri)}: {response.ResponseUri}");
            Assert.IsNotEmpty(response.Content);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);

            if (!string.IsNullOrEmpty(response.Content))
            {
                ServiceExceptionModel? serviceException = null;
                if (request.Parameters.Contains(WebRequestUtils.GetQueryParameterFormatJson()))
                {
                    serviceException = JsonConvert.DeserializeObject<ServiceExceptionModel>(response.Content);
                    Assert.IsTrue(serviceException is not null);
                }
                else if (request.Parameters.Contains(WebRequestUtils.GetQueryParameterFormatXml()))
                {
                    serviceException = new ServiceExceptionModel().DeserializeFromXml<ServiceExceptionModel>(response.Content);
                    Assert.IsTrue(serviceException is not null);
                }
                if (serviceException is not null)
                {
                    Assert.IsNotEmpty(serviceException.FilePath);
                    Assert.Greater(serviceException.LineNumber, 0);
                    Assert.IsNotEmpty(serviceException.MemberName);
                    Assert.IsNotEmpty(serviceException.Exception);
                    Assert.IsNotEmpty(serviceException.InnerException);
                }
            }
        });
    }

    public static ResponseBarCodeModel CastBarCode(BarCodeModel barCode) =>
        new ResponseBarCodeModel().CloneCast(barCode);
    
    public static List<ResponseBarCodeModel> CastBarCodes(IEnumerable<BarCodeModel> barCodes) => 
        barCodes.Select(CastBarCode).ToList();

    #endregion
}
