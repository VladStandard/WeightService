// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Net;
using DataCore.Utils;
using Newtonsoft.Json;
using NUnit.Framework;
using WsWebApi.Models;
using WsWebApi.Utils;

namespace WsWebApiTests.Utils;

public static class WebResponseUtilsTests
{
    public static async Task GetExceptionAsync(string url, RestRequest request)
    {
        await WebResponseUtils.GetResponseAsync(url, request, response =>
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
                    serviceException =  DataFormatUtils.DeserializeFromXml<ServiceExceptionModel>(response.Content); 
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

    public static async Task GetInfoAsync(string url, RestRequest request)
    {
        await WebResponseUtils.GetResponseAsync(url, request, response =>
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
                    serviceInfo = DataFormatUtils.DeserializeFromXml<ServiceInfoModel>(response.Content);
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
}