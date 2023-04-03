// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Utils;
using Newtonsoft.Json;
using NUnit.Framework;
using System.Net;
using WsWebApi.Models;
using WsWebApi.Base;
using WsWebApi.Utils;

namespace WsWebApiTests.Utils;

internal static class WebResponseUtilsTests
{
    internal static async Task GetExceptionAsync(string url, RestRequest request)
    {
        await WsWebResponseUtils.GetResponseAsync(url, request, response =>
        {
            TestContext.WriteLine($"{nameof(response.ResponseUri)}: {response.ResponseUri}");
            Assert.IsNotEmpty(response.Content);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);

            if (!string.IsNullOrEmpty(response.Content))
            {
                WsServiceExceptionModel? serviceException = null;
                if (request.Parameters.Contains(WsWebRequestUtils.GetQueryParameterFormatJson()))
                {
                    serviceException = JsonConvert.DeserializeObject<WsServiceExceptionModel>(response.Content);
                    Assert.IsTrue(serviceException is not null);
                }
                else if (request.Parameters.Contains(WsWebRequestUtils.GetQueryParameterFormatXml()))
                {
                    serviceException = DataFormatUtils.DeserializeFromXml<WsServiceExceptionModel>(response.Content);
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

    internal static async Task GetInfoAsync(string url, RestRequest request)
    {
        await WsWebResponseUtils.GetResponseAsync(url, request, response =>
        {
            TestContext.WriteLine($"{nameof(response.ResponseUri)}: {response.ResponseUri}");
            Assert.IsNotEmpty(response.Content);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);

            if (!string.IsNullOrEmpty(response.Content))
            {
                WsServiceInfoModel? serviceInfo = null;
                if (request.Parameters.Contains(WsWebRequestUtils.GetQueryParameterFormatJson()))
                {
                    serviceInfo = JsonConvert.DeserializeObject<WsServiceInfoModel>(response.Content);
                    Assert.IsTrue(serviceInfo is not null);
                }
                else if (request.Parameters.Contains(WsWebRequestUtils.GetQueryParameterFormatXml()))
                {
                    serviceInfo = DataFormatUtils.DeserializeFromXml<WsServiceInfoModel>(response.Content);
                    Assert.IsTrue(serviceInfo is not null);
                }
                if (serviceInfo is not null)
                {
                    Assert.IsNotEmpty(serviceInfo.Server);
                    Assert.IsNotEmpty(serviceInfo.App);
                    Assert.IsTrue(serviceInfo.App.StartsWith("WebApi", StringComparison.InvariantCultureIgnoreCase));
                    Assert.IsNotEmpty(serviceInfo.App);
                    Assert.IsNotEmpty(serviceInfo.Version);
                    Assert.IsNotEmpty(serviceInfo.WinCurrentDate);
                    Assert.IsNotEmpty(serviceInfo.SqlCurrentDate);
                    Assert.IsNotEmpty(serviceInfo.ConnectionString);
                    Assert.Greater(serviceInfo.ConnectTimeout, 0);
                    Assert.IsNotEmpty(serviceInfo.DataSource);
                    Assert.IsNotEmpty(serviceInfo.SqlServerVersion);
                    Assert.IsNotEmpty(serviceInfo.Database);
                    Assert.Greater(serviceInfo.PhysicalMegaBytes, 0);
                    Assert.Greater(serviceInfo.VirtualMegaBytes, 0);
                }
            }
        });
    }
}