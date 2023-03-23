// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using RestSharp;
using System;
using WsStorage.Enums;
using WsWebApi.Models.WebRequests;
using WsWebApi.Utils;
using WsWebApiTests.Utils;

namespace WsWebApiScalesTests.Controllers;

[TestFixture]
internal class TestControllerTests
{
    [Test]
    public void GetListException_Execute_DoesNotThrow()
    {
        Assert.DoesNotThrowAsync(async () =>
        {
            await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);
            //foreach (string url in new WebRequestScales().GetListException(ServerType.All))
            //{
            //    foreach (RestRequest request in WebRequestUtils.GetRequestFormats())
            //    {
            //        await WebResponseUtilsTests.GetExceptionAsync(url, request);
            //    }
            //}
        });
    }

    [Test]
    public void GetListInfo_Execute_DoesNotThrow()
    {
        Assert.DoesNotThrowAsync(async () =>
        {
            await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);
            //foreach (string url in new WebRequestScales().GetListInfo(ServerType.All))
            //{
            //    foreach (RestRequest request in WebRequestUtils.GetRequestFormats())
            //    {
            //        await WebResponseUtilsTests.GetInfoAsync(url, request);
            //    }
            //}
        });
    }
}