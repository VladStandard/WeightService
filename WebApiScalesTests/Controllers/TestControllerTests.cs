// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using RestSharp;
using WsStorageCore.Enums;
using WsWebApiCore.Models.WebRequests;
using WsWebApiCore.Utils;
using WsWebApiCoreTests.Utils;

namespace WebApiScalesTests.Controllers;

[TestFixture]
internal class TestControllerTests
{
    [Test]
    public void GetListException_Execute_DoesNotThrow()
    {
        Assert.DoesNotThrowAsync(async () =>
        {
            foreach (string url in new WebRequestScales().GetListException(ServerType.All))
            {
                foreach (RestRequest request in WebRequestUtils.GetRequestFormats())
                {
                    await WebResponseUtilsTests.GetExceptionAsync(url, request);
                }
            }
        });
    }

    [Test]
    public void GetListInfo_Execute_DoesNotThrow()
    {
        Assert.DoesNotThrowAsync(async () =>
        {
            foreach (string url in new WebRequestScales().GetListInfo(ServerType.All))
            {
                foreach (RestRequest request in WebRequestUtils.GetRequestFormats())
                {
                    await WebResponseUtilsTests.GetInfoAsync(url, request);
                }
            }
        });
    }
}